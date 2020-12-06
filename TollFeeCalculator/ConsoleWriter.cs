using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class ConsoleWriter
    {
        public void PrintTotalFee(int totalFeeCost)
        {
            Console.Write($"The total fee for the inputfile is {totalFeeCost}");
        }
    }
}
