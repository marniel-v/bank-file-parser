using System.Xml;

namespace BankFileParser.Models
{
    public class FlatHeader
    {
        public string BankName { get; set; }
        public int RecordCount { get; set; }
        public double TotalValue { get; set; }

        public FlatHeader(string bankName)
        {
            this.BankName = bankName;
            this.RecordCount = 0;
            this.TotalValue = 0;
        }

        public void AddRecord(double? value)
        {
            this.RecordCount++;
            this.TotalValue += value == null ? 0 : (double) value;
        }

        public override string ToString()
        {
            return FormatBankName() + FormatRecordCount() + FormatTotalValue();
        }

        private string FormatBankName()
        {
            var upper = this.BankName.ToUpper();

            var length = upper.Length < 16 ? upper.Length : 16;
            var shorten = upper.Substring(0, length);

            return shorten.PadRight(16, ' ');
        }

        private string FormatRecordCount()
        {
            return this.RecordCount.ToString().PadLeft(3, '0');
        }

        private string FormatTotalValue()
        {
            var toCents = this.TotalValue * 100;

            return toCents.ToString().PadLeft(10, '0');
        }
    }
}
