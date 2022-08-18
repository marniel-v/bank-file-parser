using BankFileParser.Models;
using System.Text;

namespace BankFileParser.Utils
{
    public static class GenerateTxt
    {
        public static byte[] FromFlatFile(FlatFile flatFile)
        {
            string txt = flatFile.Header.ToString() + "\r\n";

            foreach (FlatDeduction deduction in flatFile.Deductions)
            {
                txt += deduction.ToString() + "\r\n";
            }

            return Encoding.ASCII.GetBytes(txt);
        }
    }
}
