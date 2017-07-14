using System;
using System.Globalization;
using System.Configuration;

namespace CustomerLogic
{
    public class Customer : IFormattable
    {
        public string Name { get; set; } 
        public string ContactPhone { get; set; } 
        public decimal Revenue { get; set; }

        private static readonly IFormatProvider defaultNumberFormat;// = CultureInfo.GetCultureInfo("en-US");

        static Customer()
        {
            string defaultCulture = ConfigurationManager.AppSettings["defaultCulture"];
            defaultNumberFormat = CultureInfo.GetCultureInfo(defaultCulture);
        }

        public Customer(string name, string contactPhone, decimal revenue)
        {
            Name = name;
            ContactPhone = contactPhone;
            Revenue = revenue;
        }

        /// <summary>
        /// Allows to get string representation of Consumer object. Default format is en-US
        /// </summary>
        /// <returns>String with customer's data: name, revenue, phone number</returns>
        public override string ToString() => ToString(null);

        /// <summary>
        /// Allows to get format string representation of Consumer object
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns>Format string with customer's data: name, revenue, phone number</returns>
        public string ToString(IFormatProvider formatProvider) 
            => $"{Name??"NoName"}, {Revenue.ToString("C2", formatProvider??defaultNumberFormat)}, {ContactPhone??"NoNameNoPhone"}";



        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (String.IsNullOrEmpty(format))
                return ReferenceEquals(formatProvider, null) ? ToString(null) : ToString(formatProvider);

            if (formatProvider is ICustomFormatter)
                return string.Format(formatProvider, "{0:" + format + "}", this);

            switch (format.ToUpper())
            {
                case "G": return ToString();
                case "N": return Name;
                case "P": return ContactPhone;
                case "R": return $"{Revenue.ToString("C2" , formatProvider??defaultNumberFormat)}";
                case "NP": return $"{Name}, {ContactPhone}";
                case "NR": return $"{Name}, {Revenue.ToString("C2", formatProvider ?? defaultNumberFormat)}";
                case "PR": return $"{ContactPhone}, {Revenue.ToString("C2", formatProvider ?? defaultNumberFormat)}";
                default: throw new FormatException();//TODO: а на чужой формат будет ругаться? будет.
            }
        }
    }
}
