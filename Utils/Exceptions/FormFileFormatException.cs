using BankFileParser.Models;
using System.Xml;

namespace BankFileParser.Utils.Exceptions
{
    public class FormFileFormatException : Exception
    {
        public FormFileFormatException()
        {
        }

        public FormFileFormatException(string message)
            : base(message)
        {
        }

        public FormFileFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}