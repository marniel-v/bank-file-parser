using BankFileParser.Models;
using System.Xml;

namespace BankFileParser.Utils.Exceptions
{
    public class FormFileEmptyException : Exception
    {
        public FormFileEmptyException()
        {
        }

        public FormFileEmptyException(string message)
            : base(message)
        {
        }

        public FormFileEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}