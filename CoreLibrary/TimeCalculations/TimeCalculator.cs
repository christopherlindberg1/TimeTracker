using CoreLibrary.Models;
using System;
using System.Collections.Generic;

namespace CoreLibrary.TimeCalculations
{
    public class TimeCalculator
    {
        private readonly TimeSpan TimeToWorkPerDay = new TimeSpan(hours: 8, minutes: 0, seconds: 0);

        public TimeCalculator()
        {

        }

        public TimeSpan GetTimeBalanceForMonth(IEnumerable<TimeLogModel> timeLogDataForMonth)
        {
            TimeSpan balance = new TimeSpan();

            foreach (var date in timeLogDataForMonth)
            {
                if (!date.HasCompleteData)
                {
                    continue;
                }

                TimeSpan timeWorked
                    = TimeSpan.Parse(date.EndTime.ToString()) - TimeSpan.Parse(date.StartTime.ToString())
                    - new TimeSpan(hours: 0, minutes: Convert.ToInt32(date.LunchInMinutes), seconds: 0);

                balance += date.IsDayOfWeekend ? timeWorked : timeWorked - TimeToWorkPerDay;
            }

            return balance;
        }

        public TimeSpan TimeDifferencePerUpcomingDayToEvenOutBalanceForMonth(
            IEnumerable<TimeLogModel> timeLogDataForMonth)
        {
            return new TimeSpan();
        }
    }
}
