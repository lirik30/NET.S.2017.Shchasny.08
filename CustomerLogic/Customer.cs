using System;
using System.Globalization;
using System.Configuration;

namespace CustomerLogic
{
    /// <summary>
    /// In general, the uninteresting Customer class provides the ability to format its instances.
    /// </summary>
    public class Customer : IFormattable
    {
        public string Name { get; set; } 
        public string ContactPhone { get; set; } 
        public decimal Revenue { get; set; }

        /// <summary>
        /// Standard culture info. Default en-US. It is possible to configure via app.config
        /// </summary>
        public static readonly IFormatProvider defaultNumberFormat;// = CultureInfo.GetCultureInfo("en-US");

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
            => $"{Name??"NoName"}, {Revenue.ToString("C2", formatProvider??defaultNumberFormat)}, {ContactPhone??"NoPhone"}";


        /// <summary>
        /// This method allows to format the Customer's instance with format string and\or format provider
        /// </summary>
        /// <param name="format">Format string</param>
        /// <param name="formatProvider">Format provider</param>
        /// <returns>Formatted string</returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (String.IsNullOrEmpty(format))
                return ReferenceEquals(formatProvider, null) ? ToString(null) : ToString(formatProvider);

            if (formatProvider is ICustomFormatter)
                return string.Format(formatProvider, "{0:" + format + "}", this);

            switch (format.ToUpper())
            {
                case "G": return ToString();
                case "N": return Name??"NoName";
                case "P": return ContactPhone??"NoPhone";
                case "R": return $"{Revenue.ToString("C2" , formatProvider??defaultNumberFormat)}";
                case "NP": return $"{Name??"NoName"}, {ContactPhone??"NoPhone"}";
                case "NR": return $"{Name??"NoName"}, {Revenue.ToString("C2", formatProvider ?? defaultNumberFormat)}";
                case "PR": return $"{ContactPhone??"NoPhone"}, {Revenue.ToString("C2", formatProvider ?? defaultNumberFormat)}";
                default: throw new FormatException();//а на чужой формат будет ругаться? будет.
            }
        }
    }
}
