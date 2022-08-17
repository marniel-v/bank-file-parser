using BankFileParser.Models;
using BankFileParser.Utils.Exceptions;
using System.Xml;

namespace BankFileParser.Utils
{
    public static class ParseXml
    {
        public static DebitOrders FromFormFile(IFormFile formFile)
        {
            XmlDocument doc = new XmlDocument();

            var fileStream = formFile.OpenReadStream();
            
            try
            {
                doc.Load(fileStream);
            }
            catch (System.Exception)
            {
                throw new FormFileLoadException();
            }

            if (doc.DocumentElement == null)
            {
                throw new FormFileEmptyException();
            }

            if (doc.DocumentElement.Name != DebitOrders.ROOT_NAME)
            {
                throw new FormFileFormatException();
            }

            return DebitOrders.FromXmlNode(doc.DocumentElement);
        }
    }
}