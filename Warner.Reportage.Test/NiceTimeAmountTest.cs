using System;
using Warner.Reportage.ViewModels.Factories;
using Xunit;

namespace Warner.Reportage.Test
{
    public class NiceTimeAmountTest
    {
        [Fact]
        public void TestCorrectDisplayStringHours()
        {
            DateTimeOffset start =
                new DateTimeOffset(2017, 11, 10, 14, 50, 22, TimeSpan.Zero);
            DateTimeOffset finish =
                new DateTimeOffset(2017, 11, 10, 19, 50, 22, TimeSpan.Zero);
            var amount = new NiceTimeAmount(start, finish);
            string result = amount.GetDisplayValue();
            Assert.Equal(result, "5 hours");
        }

        [Fact]
        public void TestCorrectDisplayStringDays()
        {
            DateTimeOffset start =
                new DateTimeOffset(2017, 11, 10, 14, 50, 22, TimeSpan.Zero);
            DateTimeOffset finish =
                new DateTimeOffset(2017, 11, 15, 19, 50, 22, TimeSpan.Zero);
            var amount = new NiceTimeAmount(start, finish);
            string result = amount.GetDisplayValue();
            Assert.Equal(result, "5 days");
        }

        [Fact]
        public void TestCorrectDisplayStringWeeks()
        {
            DateTimeOffset start =
                new DateTimeOffset(2017, 11, 10, 14, 50, 22, TimeSpan.Zero);
            DateTimeOffset finish =
                new DateTimeOffset(2017, 12, 4, 19, 50, 22, TimeSpan.Zero);
            var amount = new NiceTimeAmount(start, finish);
            string result = amount.GetDisplayValue();
            Assert.Equal(result, "3 weeks");
        }
    }
}
