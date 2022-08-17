
using BankFileParser.Models;
using BankFileParser.Utils;
using BankFileParser.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BankFileParser.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public FileUpload BankFile { get; set; }
        public string Error { get; private set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            try
            {
                DebitOrders orders = await Task.Run(() => ParseXml.FromFormFile(BankFile.FormFile));

                var json = JsonSerializer.Serialize(orders);
                var filePath = Path.GetTempFileName();

                System.IO.File.WriteAllText(filePath, json);
                
                return RedirectToPage("./Output", new {name = Path.GetFileName(filePath)});
            }
            catch (FormFileEmptyException)
            {
                Error = "Failed to parse: bank file is empty";
            }
            catch (FormFileFormatException)
            {
                Error = "Failed to parse: bank file is in incorrect format";
            }
            catch (FormFileLoadException)
            {
                Error = "Failed to parse: bank file should be in XML format";
            }
            catch (SystemException)
            {
                return RedirectToPage("./Error");
            }

            return Page();
        }
    }

    public class FileUpload
    {
        [Required]
        [Display(Name="XML Bank File")]
        public IFormFile FormFile { get; set; }
    }
}
