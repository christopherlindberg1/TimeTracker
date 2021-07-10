using System;

namespace CoreLibrary.TimeCalculation
{
    public class TimeCalculator
    {
        private readonly TimeSpan WorkingTimePerDay = new TimeSpan(hours: 8, minutes: 0, seconds: 0);

        public TimeCalculator()
        {

        }

        public TimeSpan GetTimeBalanceInMinutesPrivate(
            DateTime startTime, DateTime finishTime, int lunchInMinutes)
        {
            TimeSpan balance = finishTime.Subtract(startTime);

            balance = balance.Subtract(new TimeSpan(hours: 0, minutes: lunchInMinutes, seconds: 0));

            return balance;
        }

        public TimeSpan GetTimeBalanceInMinutesPrivate(
            DateTime startTime, DateTime finishTime, int lunchInMinutes, int breaksInMinutes)
        {
            TimeSpan balance = finishTime.Subtract(startTime);

            balance = balance.Subtract(new TimeSpan(hours: 0, minutes: lunchInMinutes, seconds: 0));
            balance = balance.Subtract(new TimeSpan(hours: 0, minutes: breaksInMinutes, seconds: 0));

            return balance;
        }
    }
}
