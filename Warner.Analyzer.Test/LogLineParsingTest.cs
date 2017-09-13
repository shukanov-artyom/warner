using System;
using Warner.Analyzer.LogWrapper;
using Xunit;

namespace Warner.Analyzer.Test
{
    public class LogLineParsingTest
    {
        private string repo = @"d:\dev\ORTB-MERGE-WEBSITES\";

        [Theory]
        [InlineData(@"build	13-Mar-2017 15:27:22	Models\Evidence\PostDetails.cs(41,23): warning CS0108: 'PostDetails.Specialty' hides inherited member 'PostItem.Specialty'. Use the new keyword if hiding was intended. [d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Presentation\Orthobullets.Presentation.csproj]")]
        public void ParseWarningLineCs0108(string line)
        {
            LogEntryParser parser = new LogEntryParser(line);
            Assert.Equal(parser.IsWarningEntry(), true);
            LogEntryInfo result = parser.Parse(repo);
            Assert.Equal(result.WarningType, "CS0108".ToLower());
            Assert.Equal(result.LoggedDate, DateTime.Parse("13-Mar-2017 15:27:22"));
            Assert.Equal(result.SourceFilePathName, @"Orthobullets.Presentation\Models\Evidence\PostDetails.cs");
            Assert.Equal(result.CodeLineNumber, 41);
        }

        [Theory]
        [InlineData(@"build	13-Mar-2017 15:39:17	d:\dev\ORTB-MERGE-WEBSITES\website\App_Code\Repository\CacheRepository\VideoCacheRepository.cs(31): warning CS0618: 'Helpers.CacheHelper' is obsolete: 'This helper should be removed. Use Orthobullets.Domain.Helpers.CacheHelper instead.' [d:\dev\ORTB-MERGE-WEBSITES\Build.Targets]")]
        public void ParseWarningLineCs618(string line)
        {
            LogEntryParser parser = new LogEntryParser(line);
            Assert.Equal(parser.IsWarningEntry(), true);
            LogEntryInfo result = parser.Parse(repo);
            Assert.Equal(result.WarningType, "CS0618".ToLower());
            Assert.Equal(result.LoggedDate, DateTime.Parse("13-Mar-2017 15:39:17"));
            Assert.Equal(result.SourceFilePathName, @"website\App_Code\Repository\CacheRepository\VideoCacheRepository.cs");
            Assert.Equal(result.CodeLineNumber, 31);
        }

        [Theory]
        [InlineData(@"build	15-Mar-2017 10:35:03	d:\dev\ORTB-MERGE-WEBSITES\Othobullets.Data\Extensions\MultipleResultSets.cs(52): warning CA2100: Microsoft.Security : The query string passed to 'DbCommand.CommandText.set(string)' in 'MultipleResultSets.MultipleResultSetWrapper.Execute()' could contain the following variables 'this._storedProcedure'. If any of these variables could come from user input, consider using a stored procedure or a parameterized SQL query instead of building the query with string concatenations. [d:\dev\ORTB-MERGE-WEBSITES\Othobullets.Data\Orthobullets.Data.csproj]")]
        public void ParseWarningCA(string line)
        {
            LogEntryParser parser = new LogEntryParser(line);
            Assert.Equal(parser.IsWarningEntry(), true);
            LogEntryInfo result = parser.Parse(repo);
            Assert.Equal(result.WarningType, "CA2100".ToLower());
            Assert.Equal(result.LoggedDate, DateTime.Parse("15-Mar-2017 10:35:03"));
            Assert.Equal(result.SourceFilePathName, @"Othobullets.Data\Extensions\MultipleResultSets.cs");
            Assert.Equal(result.CodeLineNumber, 52);
        }

        [Theory]
        [InlineData(@"build	15-Mar-2017 10:35:03	d:\dev\ORTB-MERGE-WEBSITES\Othobullets.Data\Constants.cs(8,1): warning : SA1515 : CSharp.Layout : A single-line comment must be preceded by a blank line or another single-line comment, or must be the first item in its scope. To ignore this error when commenting out a line of code, begin the comment with '////' rather than '//'. [d:\dev\ORTB-MERGE-WEBSITES\Othobullets.Data\Orthobullets.Data.csproj]")]
        public void ParseWarningSA(string line)
        {
            LogEntryParser parser = new LogEntryParser(line);
            Assert.Equal(parser.IsWarningEntry(), true);
            LogEntryInfo result = parser.Parse(repo);
            Assert.Equal(result.WarningType, "SA1515".ToLower());
            Assert.Equal(result.LoggedDate, DateTime.Parse("15-Mar-2017 10:35:03"));
            Assert.Equal(result.SourceFilePathName, @"Othobullets.Data\Constants.cs");
            Assert.Equal(result.CodeLineNumber, 8);
        }

        [Theory]
        [InlineData(@"build	01-Apr-2017 00:03:01	d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Caching\Caches.cs(11,1): warning : SA1609 : CSharp.Documentation : The public or protected property's documentation header must contain a value tag. [d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Orthobullets.Common.csproj]")]
        public void ParseWarningSaMore(string line)
        {
            LogEntryParser parser = new LogEntryParser(line);
            Assert.Equal(parser.IsWarningEntry(), true);
            LogEntryInfo result = parser.Parse(repo);
            Assert.Equal(result.WarningType, "SA1609".ToLower());
            Assert.Equal(result.LoggedDate, DateTime.Parse("01-Apr-2017 00:03:01"));
            Assert.Equal(result.SourceFilePathName, @"Orthobullets.Common\Caching\Caches.cs");
            Assert.Equal(result.CodeLineNumber, 11);
        }

        [Fact]
        public void ProcessEmptyLine()
        {
            var parser = new LogEntryParser(String.Empty);
            Assert.Equal(parser.IsWarningEntry(), false);
        }

        [Theory]
        [InlineData(
            @"build	30-Mar-2017 00:02:32	TestQuestionPermissionService\TestQuestionFlags\GetAccessibleTestQuestionFlagsTest.cs(22,69): warning CS0649: Field 'GetAccessibleTestQuestionFlagsTest._service' is never assigned to, and will always have its default value null [d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Tests.BDD\Orthobullets.Tests.BDD.csproj]",
            @"Orthobullets.Tests.BDD\TestQuestionPermissionService\TestQuestionFlags\GetAccessibleTestQuestionFlagsTest.cs")]
        [InlineData(
            @"build	30-Mar-2017 00:03:43	d:\dev\ORTB-MERGE-WEBSITES\website\App_Code\EdBullets\Repository\EmailNotificationSettingsRepository.cs(48): warning CS0618: 'Helpers.CacheHelper' is obsolete: 'This helper should be removed. Use Orthobullets.Domain.Helpers.CacheHelper instead.' [d:\dev\ORTB-MERGE-WEBSITES\Build.Targets]",
            @"website\App_Code\EdBullets\Repository\EmailNotificationSettingsRepository.cs")]
        [InlineData(
            @"build	19-Apr-2017 13:13:17	d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Enums\FastEnum.cs(22,1): warning : SA1622 : CSharp.Documentation : The documentation text within the typeparam tag for the '<typeparam name=""T"" />' parameter must not be empty. [d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Orthobullets.Common.csproj]",
            @"Orthobullets.Common\Enums\FastEnum.cs")]
        [InlineData(
            @"build	19-Apr-2017 13:25:25	  d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Caching\Caches.cs(11,1): warning : SA1609 : CSharp.Documentation : The public or protected property's documentation header must contain a value tag. [d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Orthobullets.Common.csproj]",
            @"Orthobullets.Common\Caching\Caches.cs")]
        [InlineData(
            @"build	19-Apr-2017 13:25:25	  Enums\UserRoleEnum.cs(14,62): warning CS0618: 'WebsiteType.Medbullets' is obsolete: 'Use either MedbulletsStep1 or MedbulletsStep2And3' [d:\dev\ORTB-MERGE-WEBSITES\Orthobullets.Common\Orthobullets.Common.csproj]",
            @"Orthobullets.Common\Enums\UserRoleEnum.cs")]
        [InlineData(
            @"build	08-Aug-2017 11:55:15	  d:\dev\ORTB-MERGE-WEBSITES\Othobullets.Data\Common\TakenItems`1.cs(7): warning CA2227: Microsoft.Usage : Change 'TakenItems<T>.Items' to be read-only by removing the property setter. [d:\dev\ORTB-MERGE-WEBSITES\Othobullets.Data\Orthobullets.Data.csproj]",
            @"Othobullets.Data\Common\TakenItems`1.cs")]
        public void ParseCsType1(string data, string prime)
        {
            var parser = new LogEntryParser(data);
            Assert.Equal(parser.IsWarningEntry(), true);
            LogEntryInfo result = parser.Parse(repo);
            Assert.Equal(result.SourceFilePathName, prime);
        }
    }
}
