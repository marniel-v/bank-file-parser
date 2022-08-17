using BankFileParser.Models;
using System.Xml;

namespace BankFileParser.Utils.Exceptions
{
    public class FormFileLoadException : Exception
    {
        public FormFileLoadException()
        {
        }

        public FormFileLoadException(string message)
            : base(message)
        {
        }

        public FormFileLoadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}