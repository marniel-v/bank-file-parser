using BankFileParser.Models.Exceptions;
using System.Xml;
using System.Collections.Generic;

namespace BankFileParser.Models
{
    public class FlatFile
    {
        public FlatHeader Header { get; set; }
        public List<FlatDeduction> Deductions { get; set; }

        public FlatFile(FlatHeader header)
        {
            this.Header = header;
            this.Deductions = new List<FlatDeduction>();
        }

        public void AddDeduction(Deduction deduction)
        {
            if (!deduction.IsValid()) {
                throw new DeductionInvalidFormatException();
            }

            this.Header.AddRecord(deduction.Amount);

            FlatDeduction flat = new FlatDeduction(deduction);
            this.Deductions.Add(flat);
        }

        public void SortDeductions()
        {
            this.Deductions.Sort();
        }
    }
}
