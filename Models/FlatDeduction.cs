using System.Collections;
using System.Globalization;
using System.Xml;

namespace BankFileParser.Models
{
    public class FlatDeduction: IComparable<FlatDeduction>
    {
        private const string ACCOUNT_TYPE_CHEQUE = "cheque";
        private const string ACCOUNT_TYPE_SAVINGS = "savings";
        private const string ACCOUNT_TYPE_CREDIT_CARD = "credit card";

        private const string ACCOUNT_TYPE_ABBR_CHEQUE = "CH";
        private const string ACCOUNT_TYPE_ABBR_SAVINGS = "SAV";
        private const string ACCOUNT_TYPE_ABBR_CREDIT_CARD = "CR";
        private const string ACCOUNT_TYPE_ABBR_OTHER = "OTH";

        public char Initials { get; set; }
        public string Surname { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Branch { get; set; }
        public double? Amount { get; set; }
        public string Date { get; set; }

        public FlatDeduction(Deduction deduction)
        {
            string[] holderSplit = deduction.AccountHolder.Split(" ");

            this.Initials = holderSplit[0][0];
            this.Surname = string.Join("", holderSplit, 1, holderSplit.Length - 1);
            this.AccountNumber = deduction.AccountNumber;
            this.AccountType = deduction.AccountType;
            this.Branch = deduction.Branch;
            this.Amount = deduction.Amount;
            this.Date = deduction.Date;
        }

        public string ToString()
        {
            return this.Initials + 
                FormatSurname() +
                FormatAccountNumber() +
                FormatAccountType() +
                FormatBranch() +
                FormatAmount() +
                FormatDate();
        }

        private string FormatSurname()
        {
            return this.Surname.Replace(" ", "").PadRight(15, ' ');
        }

        private string FormatAccountNumber()
        {
            return this.AccountNumber.PadRight(14, ' ');
        }

        private string FormatAccountType()
        {
            var abbr = "";

            switch (this.AccountType)
            {
                case ACCOUNT_TYPE_CHEQUE:
                    abbr = ACCOUNT_TYPE_ABBR_CHEQUE;
                    break;
                case ACCOUNT_TYPE_SAVINGS:
                    abbr = ACCOUNT_TYPE_ABBR_SAVINGS;
                    break;
                case ACCOUNT_TYPE_CREDIT_CARD:
                    abbr = ACCOUNT_TYPE_ABBR_CREDIT_CARD;
                    break;
                default:
                    abbr = ACCOUNT_TYPE_ABBR_OTHER;
                    break;
            }

            return abbr.PadRight(3, ' ');
        }

        private string FormatBranch()
        {
            return this.Branch.PadRight(10, ' ');
        }

        private string FormatAmount()
        {
            var toCents = this.Amount == null ? 0 : ((double) this.Amount) * 100;

            return toCents.ToString().PadLeft(7, '0');
        }

        private string FormatDate()
        {
            var dateTime = DateTime.Now;

            if (!DateTime.TryParseExact(this.Date, "dd/MM/yyyy", null, DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.Parse(this.Date);
            }

            return dateTime.ToString("ddyyyyMM");
        }

        public int CompareTo(FlatDeduction other)
        {
            var thisAmount = this.Amount == null ? 0 : (double) this.Amount;
            var otherAmount = other.Amount == null ? 0 : (double) other.Amount;

            if (thisAmount == otherAmount)
            {
                return this.Surname.CompareTo(other.Surname);
            }
            
            return thisAmount.CompareTo(otherAmount);
            
        }
    }
}
