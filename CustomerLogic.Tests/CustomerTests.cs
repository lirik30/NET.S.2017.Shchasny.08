using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;

namespace CustomerLogic.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        private static IEnumerable<TestCaseData> ToStringTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                    new Customer("Jeffrey Richter", "+ 1 (425) 555-0100", 1000000),
                    "G",
                    null
                ).Returns("Jeffrey Richter, $1,000,000.00, + 1 (425) 555-0100");//?

                yield return new TestCaseData(
                    new Customer("Petya Petrov", "+ 375-00-111-2233", 12356),
                    "R",
                    CultureInfo.GetCultureInfo("en-GB")
                    ).Returns("£12,356.00");

                yield return new TestCaseData(
                    new Customer("Ivan Ivanov", "+ 375-99-888-77-66", 50),
                    "R",
                    CultureInfo.GetCultureInfo("ru-RU")
                    ).Returns("50,00 ₽");

                yield return new TestCaseData(
                    new Customer(null, null, 0),
                    "N",
                    null
                    ).Returns("NoName");

                yield return new TestCaseData(
                    new Customer("Gleb Glebov", "+ 192837465", 0),
                    "W",
                    new PhoneWordyFormatProvider()
                    ).Returns("Gleb Glebov phone number: plus one nine two eight three seven four six five");
            }
        }

        [Test, TestCaseSource(nameof(ToStringTestData_Positive))]
        public string ToStringFormatter_PositiveTests(Customer customer, string format, IFormatProvider formatProvider)
        {
            return customer.ToString(format, formatProvider);
        }


        private static IEnumerable<TestCaseData> ToStringTestData_ThrowsFormatException
        {
            get
            {
                yield return new TestCaseData(
                    new Customer("Jeffrey Richter", "+ 1 (425) 555-0100", 1000000),
                    "T",
                    null);

                yield return new TestCaseData(
                    new Customer("Petya Petrov", "+ 375-00-111-2233", 12356),
                    "K",
                    new PhoneWordyFormatProvider());
            }
        }

        [Test, TestCaseSource(nameof(ToStringTestData_ThrowsFormatException))]
        public void ToStringFormatter_ThrowsFormatException(Customer customer, string format, IFormatProvider formatProvider)
        {
            Assert.Throws<FormatException>(() => customer.ToString(format, formatProvider));
        }
    }
}
