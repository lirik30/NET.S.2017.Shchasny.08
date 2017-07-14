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
                ).Returns("Jeffrey Richter, $1,000,000.00, + 1 (425) 555-0100");

                yield return new TestCaseData(
                    new Customer("Petya Petrov", "+ 375-00-111-2233", 12356),
                    "R",
                    CultureInfo.GetCultureInfo("en-GB")
                    ).Returns("£12,356.00");

                yield return new TestCaseData().Returns();
                //yield return new TestCaseData();

                //yield return new TestCaseData();
            }
        }

        [Test, TestCaseSource(nameof(ToStringTestData_Positive))]
        public string ToStringFormatter_PositiveTests(Customer customer, string format, IFormatProvider formatProvider)
        {
            return customer.ToString(format, formatProvider);
        }
    }
}
