using BankFileParser.Models;
using BankFileParser.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO.Compression;
using System.Text.Json;

namespace BankFileParser.Pages;

public class OutputModel : PageModel
{
    public DebitOrders Orders { get; private set; }
    public string OrdersFileName { get; private set; }
    public Dictionary<string, FlatFile> FlatFiles { get; private set; }
    public string Error { get; private set; }

    public IActionResult OnGet(string fileName)
    {
        OrdersFileName = fileName;
        
        try
        {
            InitOrders();
            InitFlatFiles("all");
        }
        catch (System.Exception)
        {
            Error = String.IsNullOrEmpty(Error) ? "An error occurred while processing your request" : Error;

            return RedirectToPage("./Error", new { error = Error});
        }

        return Page();
    }

    public IActionResult OnGetDownload(string fileName, string flatName)
    {
        OrdersFileName = fileName;

        try
        {
            InitOrders();
            InitFlatFiles(flatName);
        }
        catch (System.Exception)
        {
            return RedirectToPage("./Error", new { error = "An error occurred while processing your request"});
        }

        if (FlatFiles.Count() > 1) {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(outputStream, ZipArchiveMode.Create, true))
                {
                    foreach (KeyValuePair<string, FlatFile> entry in FlatFiles)
                    {
                        var txtEntry = archive.CreateEntry(entry.Value.Header.BankName + ".txt");
                        byte[] flatBytes = Utils.GenerateTxt.FromFlatFile(entry.Value);

                        using (var archiveStream = txtEntry.Open())
                        using (var compressStream  = new MemoryStream(flatBytes))
                        {
                            compressStream.CopyTo(archiveStream);
                        }
                    }
                }

                return File(outputStream.ToArray(), "application/octet-stream", "FlatFiles.zip");
            }
        }

        byte[] bytes = Utils.GenerateTxt.FromFlatFile(FlatFiles.First().Value);

        return File(bytes, "application/octet-stream", flatName + ".txt");
    }

    private void InitOrders()
    {
        Orders = new DebitOrders();

        var ordersPath = Path.GetTempPath() + $"{OrdersFileName}";
        var json = System.IO.File.ReadAllText(ordersPath);

        Orders = JsonSerializer.Deserialize<DebitOrders>(json);
    }

    private void InitFlatFiles(string flatName)
    {
        FlatFiles = new Dictionary<string, FlatFile>();

        if (Orders == null)
        {
            throw new Exception("DebitOrders has not been initialised");
        }

        foreach (Deduction deduction in Orders.Deductions)
        {
            if (flatName != "all" && deduction.BankName != flatName) {
                continue;
            }

            if (!FlatFiles.ContainsKey(deduction.BankName)){
                FlatHeader header = new FlatHeader(deduction.BankName);
                FlatFiles.Add(deduction.BankName, new FlatFile(header));
            }

            FlatFile flatFile = FlatFiles[deduction.BankName];
            
            try
            {
                flatFile.AddDeduction(deduction);
            }
            catch (DeductionInvalidFormatException)
            {
                Error = "Some bank flat file records could not be generated. View parsed file for highligted issues.";
            }
        }

        foreach (KeyValuePair<string, FlatFile> entry in FlatFiles)
        {
            entry.Value.SortDeductions();
        }
    }
}

