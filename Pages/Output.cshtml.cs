using BankFileParser.Models;
using BankFileParser.Models.Exceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;

namespace BankFileParser.Pages;

public class OutputModel : PageModel
{
    public DebitOrders Orders { get; private set; }
    public Dictionary<string, FlatFile> FlatFiles { get; private set; }
    public string Error { get; private set; }

    public void OnGet(string name)
    {
        Orders = new DebitOrders();
        FlatFiles = new Dictionary<string, FlatFile>();

        var ordersPath = Path.GetTempPath() + $"{name}";
        
        try
        {
            var json = System.IO.File.ReadAllText(ordersPath);

            Orders = JsonSerializer.Deserialize<DebitOrders>(json);

            if (Orders == null) 
            {
                Error = "Failed to deserialise bank debit orders";
            }

            foreach (Deduction deduction in Orders.Deductions)
            {
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
        catch (System.Exception)
        {
            Error = "Failed to read bank debit orders from Temp Directory";
        }
    }
}

