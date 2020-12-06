using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TollFeeCalculator;

namespace TollFeeCalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TotalFeeCostTest()
        {
            var calculator = new Calculator();
            var dates = new List<DateTime>()
            {
                DateTime.Parse("2020-06-30 10:13"),
                DateTime.Parse("2020-06-30 10:25"),
                DateTime.Parse("2020-06-30 11:04"),
            };
            var expected = 8;

            var actual = calculator.TotalFeeCost(dates);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaxTollFeeCostTest()
        {
            var calculator = new Calculator();

            var expected = 60;

            var dateTimeList = new List<DateTime>()
            {
                new DateTime(2020, 06, 30, 06, 45, 00),
                new DateTime(2020, 06, 30, 07, 55, 00),
                new DateTime(2020, 06, 30, 15, 55, 00),
                new DateTime(2020, 06, 30, 16, 56, 00),
                new DateTime(2020, 06, 30, 17, 57, 00)
            };

            var actual = calculator.TotalFeeCost(dateTimeList);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnderMaxTollFeeTest()
        {
            var calculator = new Calculator();

            var expected = 49;

            var dateTimeList = new List<DateTime>()
            {
                new DateTime(2020, 06, 30, 06, 45, 00),
                new DateTime(2020, 06, 30, 07, 55, 00),
                new DateTime(2020, 06, 30, 15, 55, 00)
            };

            var actual = calculator.TotalFeeCost(dateTimeList);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TollFeePassTest()
        {
            var calculator = new Calculator();

            var passingDateTimes = new Dictionary<DateTime, int>()
                {
                     { DateTime.Parse("2020-09-28 00:00"), 0  },
                     { DateTime.Parse("2020-09-28 06:10"), 8  },
                     { DateTime.Parse("2020-09-28 06:40"), 13 },
                     { DateTime.Parse("2020-09-28 07:10"), 18 },
                     { DateTime.Parse("2020-09-28 08:20"), 13 },
                     { DateTime.Parse("2020-09-29 09:20"), 8  },
                     { DateTime.Parse("2020-09-29 15:20"), 13 },
                     { DateTime.Parse("2020-09-29 16:10"), 18 },
                     { DateTime.Parse("2020-09-29 17:40"), 13 },
                     { DateTime.Parse("2020-09-30 18:10"), 8  },
                     { DateTime.Parse("2020-09-30 18:50"), 0  }
                };

            foreach (var dateTime in passingDateTimes)
            {
                Assert.AreEqual(dateTime.Value, calculator.TollFeePass(dateTime.Key));
            }
        }

        [TestMethod]
        public void TollPassingTimeDiffTest()
        {
            var calculator = new Calculator();
            var expected = 138;

            var dateTimes = new List<DateTime>()
            {
                new DateTime(2020, 06, 30, 08, 52, 00),
                new DateTime(2020, 06, 30, 06, 34, 00)
            };

            Assert.AreEqual(expected, calculator.TollPassingTimeDiff(dateTimes[0], dateTimes[1]).TotalMinutes);
		}

        [TestMethod]
        public void PrintTotalFeeTest()
        {
            ConsoleWriter consoleWriter = new ConsoleWriter();
            string actual;

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                consoleWriter.PrintTotalFee(500);
                actual = stringWriter.ToString();
            }

            Assert.AreEqual("The total fee for the inputfile is 500", actual);
        }

        [TestMethod]
        public void ReadFileFromDirectoryTest()
        {
            var fileReader = new FileReader();

            var fileToRead = Environment.CurrentDirectory + "../../../../test123Data.txt";

            var fileToReadTwo = Environment.CurrentDirectory + "../../../../testDataTest.txt";

            var expected = "2020-06-30 00:05";
            var actual = fileReader.ReadFileFromDirectory(fileToReadTwo);

            Assert.ThrowsException<DirectoryNotFoundException>(() => (fileReader.ReadFileFromDirectory(fileToRead)));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeParserReadableDatesTest()
        {
            var fileReader = new FileReader();
            var dateString = "2020-06-jj 00:05, 2020-06-30 ssevr06:34, 2020-06-30 08:52, 2020-06-30 10:13";
            var expected = new List<DateTime>()
            {
                DateTime.MinValue,
                DateTime.MinValue,
                new DateTime(2020, 06, 30, 08, 52, 00),
                new DateTime(2020, 06, 30, 10, 13, 00),
            };

            var actual = fileReader.DateTimeParser(dateString);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CostDifferenceTest()
        {
            var calculator = new Calculator();

            var passingCost = 13;
            var startingIntervalCost = 18;
            var expected = 5;

            var actual = calculator.CostDifference(passingCost, startingIntervalCost);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FreeTollFeeTest()
        {
            var calculator = new Calculator();

            var freeMonth = new DateTime(2020, 07, 01);
            var sunday = new DateTime(2020, 06, 28);
            var tuesday = new DateTime(2020, 06, 30);

            Assert.IsTrue(calculator.FreeTollFee(sunday));
            Assert.IsTrue(calculator.FreeTollFee(freeMonth));
            Assert.IsFalse(calculator.FreeTollFee(tuesday));
        }
    }
}
