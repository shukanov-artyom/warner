using System;

namespace Warner.Reportage.ViewModels.Factories
{
    public class NiceTimeAmount
    {
        private readonly DateTimeOffset start;
        private readonly DateTimeOffset finish;

        public NiceTimeAmount(
            DateTimeOffset start,
            DateTimeOffset finish)
        {
            this.start = start;
            this.finish = finish;
        }

        public string GetDisplayValue()
        {
            double hoursDelta = (finish - start).TotalHours;
            double daysDelta = (finish - start).TotalDays;
            double weeksDelta = daysDelta / 7;
            if (hoursDelta <= 24)
            {
                return $"{Math.Round(hoursDelta)} hours";
            }
            if (daysDelta <= 7)
            {
                return $"{Math.Round(daysDelta)} days";
            }
            if (daysDelta > 7)
            {
                return $"{Math.Round(weeksDelta)} weeks";
            }
            if (weeksDelta > 5)
            {
                return $"{Math.Round(weeksDelta / 4)} months";
            }
            throw new InvalidOperationException("How did you get here?");
        }
    }
}
