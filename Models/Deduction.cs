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
                switch (childNode.Name)
                {
                    case FIELD_ACCOUNT_HOLDER:
                        debitOrder.AccountHolder = childNode.InnerText;
                        break;
                    case FIELD_ACCOUNT_NUMBER:
                        debitOrder.AccountNumber = childNode.InnerText;
                        break;
                    case FIELD_ACCOUNT_TYPE:
                        debitOrder.AccountType = childNode.InnerText;
                        break;
                    case FIELD_BANK_NAME:
                        debitOrder.BankName = childNode.InnerText;
                        break;
                    case FIELD_BRANCH:
                        debitOrder.Branch = childNode.InnerText;
                        break;
                    case FIELD_AMOUNT:
                        try
                        {
                            debitOrder.Amount = Convert.ToDouble(childNode.InnerText);
                        }
                        catch (System.Exception)
                        {
                            // leave as null
                        }
                        break;
                    case FIELD_DATE:
                        debitOrder.Date = childNode.InnerText;
                        break;
                    default:
                        break;
                }
            }

            return debitOrder;
        }
    }
}