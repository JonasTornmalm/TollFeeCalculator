using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Calculator
    {
        public int TotalFeeCost(List<DateTime> TollDateTimes)
        {
            int totalFee = 0;
            DateTime startingInterval = TollDateTimes[0]; //Starting interval
            foreach (var timeOfPassage in TollDateTimes)
            {
                var timeDiff = TollPassingTimeDiff(timeOfPassage, startingInterval);
                if (timeDiff.TotalMinutes > 60 || timeOfPassage == TollDateTimes[0])
                {
                    totalFee += TollFeePass(timeOfPassage);
                    startingInterval = timeOfPassage;
                }
                else
                {
                    int costDifference = CostDifference(TollFeePass(timeOfPassage), TollFeePass(startingInterval));
                    totalFee += costDifference;
                }
            }
            return Math.Min(totalFee, 60);
        }

        public TimeSpan TollPassingTimeDiff(DateTime timeOfPassage, DateTime startingInterval)
        {
            TimeSpan diffInMinutes = (timeOfPassage - startingInterval);
            return diffInMinutes;
        }

        public int CostDifference(int passingCost, int startingIntervalCost)
        {
            int differenceInCost = passingCost - startingIntervalCost;
            if (differenceInCost < 0)
            {
                return startingIntervalCost - passingCost;
            }
            return differenceInCost;
        }

        public int TollFeePass(DateTime currentPassing)
        {
            if (FreeTollFee(currentPassing)) return 0;
            int hour = currentPassing.Hour;
            int minute = currentPassing.Minute;
            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 8 && minute >= 30 && minute <= 59) return 8;
            else if (hour >= 9 && hour <= 14) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }
        //Gets free dates
        public bool FreeTollFee(DateTime day)
        {
            return (int)day.DayOfWeek == 0 || (int)day.DayOfWeek == 6 || day.Month == 7;
        }
    }
}
