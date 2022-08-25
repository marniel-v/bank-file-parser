using System.Globalization;
using System.Xml;

namespace BankFileParser.Models
{
    public class Deduction
    {
        public const string FIELD_ACCOUNT_HOLDER = "accountholder";
        public const string FIELD_ACCOUNT_NUMBER = "accountnumber";
        public const string FIELD_ACCOUNT_TYPE = "accounttype";
        public const string FIELD_BANK_NAME = "bankname";
        public const string FIELD_BRANCH = "branch";
        public const string FIELD_AMOUNT = "amount";
        public const string FIELD_DATE = "date";

        public string? AccountHolder { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public double? Amount { get; set; }
        public string? Date { get; set; }

        public static Deduction FromXmlNode(XmlNode node)
        {
            Deduction debitOrder = new Deduction();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                var innerText = String.IsNullOrWhiteSpace(childNode.InnerText) ? null : childNode.InnerText;
 
                switch (childNode.Name)
                {
                    case FIELD_ACCOUNT_HOLDER:
                        debitOrder.AccountHolder = innerText;
                        break;
                    case FIELD_ACCOUNT_NUMBER:
                        debitOrder.AccountNumber = innerText;
                        break;
                    case FIELD_ACCOUNT_TYPE:
                        debitOrder.AccountType = innerText;
                        break;
                    case FIELD_BANK_NAME:
                        debitOrder.BankName = innerText;
                        break;
                    case FIELD_BRANCH:
                        debitOrder.Branch = innerText;
                        break;
                    case FIELD_AMOUNT:
                        try
                        {
                            debitOrder.Amount = innerText == null ? null : Convert.ToDouble(innerText, CultureInfo.InvariantCulture);
                        }
                        catch (System.Exception)
                        {
                            // leave as null
                        }
                        break;
                    case FIELD_DATE:
                        var result = DateTime.Now;

                        if (DateTime.TryParse(innerText, null, DateTimeStyles.None, out result))
                        {
                            debitOrder.Date = innerText;
                        }
                        break;
                    default:
                        break;
                }
            }

            return debitOrder;
        }

        public bool IsValid()
        {
            return this.AccountHolder != null &&
                this.AccountNumber != null &&
                this.AccountType != null &&
                this.BankName != null &&
                this.Branch != null &&
                this.Amount != null &&
                this.Date != null;
        }
    }
}
