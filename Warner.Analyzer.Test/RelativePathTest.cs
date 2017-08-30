using System;
using Warner.Analyzer.LogWrapper.WarnTypeParsers;
using Xunit;

namespace Warner.Analyzer.Test
{
    public class RelativePathTest
    {
        [Theory]
        [InlineData(
            @"e:\\ob-mrg-job1\\orthobullets.common\\enums\\enumdisplayorderattribute.cs",
            @"e:\\ob-mrg-job1\\",
            @"orthobullets.common\\enums\\enumdisplayorderattribute.cs")]
        public void Test(
            string absolutePath,
            string partToCutOut,
            string expectedResult)
        {
            RelativePath rp = new RelativePath(partToCutOut);
            string result = rp.GetFromAbsolute(absolutePath);
            Assert.Equal(result, expectedResult);
        }
    }
}
