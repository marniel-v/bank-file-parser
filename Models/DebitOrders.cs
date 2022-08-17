using System.Xml;

namespace BankFileParser.Models
{
    public class DebitOrders
    {
        public const string ROOT_NAME = "debitorders";
        public const string FIELD_DEDUCTION = "deduction";

        public List<Deduction> Deductions { get; set; }

        public DebitOrders()
        {
            Deductions = new List<Deduction>();
        }

        public static DebitOrders FromXmlNode(XmlNode node)
        {
            DebitOrders debitOrders = new DebitOrders();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name == FIELD_DEDUCTION) {
                    Deduction deduction = Deduction.FromXmlNode(childNode);
                    debitOrders.Deductions.Add(deduction);
                }
            }

            return debitOrders;
        }
    }
}
