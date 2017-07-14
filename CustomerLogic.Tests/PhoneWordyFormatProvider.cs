using System;
using System.Text;

namespace CustomerLogic.Tests
{
    class PhoneWordyFormatProvider : IFormatProvider, ICustomFormatter
    {
        static readonly string[] numberWords =
            "zero one two three four five six seven eight nine plus".Split();

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(arg is Customer) || format.ToUpper() != "W")
                return IfOtherFormat(format, arg);
            var customer = arg as Customer;

            StringBuilder result = new StringBuilder($"{customer.Name} pnone number: ");
            foreach (char c in customer.ContactPhone)
            {
                int i = "0123456789+".IndexOf(c);
                if (i == -1) continue;
                if (result.Length > 0) result.Append(' ');
                result.Append(numberWords[i]);
            }

            return result.ToString();
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        private string IfOtherFormat(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, null);
            if (arg != null)
                return arg.ToString();
            return String.Empty;
        }
    }
}
