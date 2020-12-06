using System;
using System.Collections.Generic;
using System.IO;

namespace TollFeeCalculator
{
    public class Program
    {
        static void Main()
        {
            var fileReader = new FileReader();
            var calculator = new Calculator();
            var consoleWriter = new ConsoleWriter();

            var fileToRead = Environment.CurrentDirectory + "../../../../testData.txt";

            var textInFile = fileReader.ReadFileFromDirectory(fileToRead);
            var tollDateTimes = fileReader.DateTimeParser(textInFile);
            var totalFeeCost = calculator.TotalFeeCost(tollDateTimes);

            consoleWriter.PrintTotalFee(totalFeeCost);
        }
    }
}
