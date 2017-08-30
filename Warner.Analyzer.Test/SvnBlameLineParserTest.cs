using System;
using Warner.Analyzer.Repository;
using Xunit;

namespace Warner.Analyzer.Test
{
    public class SvnBlameLineParserTest
    {
        [Theory]
        [InlineData(
            @" 11466 aliaksandr_viarheichyk 2016-12-22 17:54:01 +0300 (Thu, 22 Dec 2016) Project({FAE04EC0 - 301F - 11D3 - BF4B - 00C04F79EFBC}) = Medtryx.Presentation.Shared, .Presentation.Shared\Medtryx.Presentation.Shared.csproj, {44D9FC35-B63B-4CC5-825A-7165FBE30276}",
            "aliaksandr_viarheichyk")]
        [InlineData(
            @"   344      rande 2012-11-01 23:58:04 +0300 (Thu, 01 Nov 2012)         IsViewCountVisible = false;",
            "rande")]
        public void TestParsing(string line, string expectedDeveloper)
        {
            var parser = new SvnBlameLineParser(line);
            var result = parser.Parse();
            Assert.Equal(result.DeveloperName, expectedDeveloper);
        }
    }
}
