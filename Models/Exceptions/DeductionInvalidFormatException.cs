using BankFileParser.Models;
using System.Xml;

namespace BankFileParser.Models.Exceptions
{
    public class DeductionInvalidFormatException : Exception
    {
        public DeductionInvalidFormatException()
        {
        }

        public DeductionInvalidFormatException(string message)
            : base(message)
        {
        }

        public DeductionInvalidFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
