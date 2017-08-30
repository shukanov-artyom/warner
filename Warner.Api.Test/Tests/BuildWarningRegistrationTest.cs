using System;
using Warner.Api.Commands.WarningReport;
using Warner.Api.Test.Cqrs.Commands;
using Warner.Domain;
using Xunit;

namespace Warner.Api.Test.Tests
{
    public class BuildWarningRegistrationTest : CqrsUnitTestClass
    {
        private readonly TestCommandDispatcher commandDispatcher;

        public BuildWarningRegistrationTest()
        {
            commandDispatcher = new TestCommandDispatcher();
            InitializeContext();
        }

        [Fact]
        public void TestWarningRegistration()
        {
            BuildWarning warning = CreateTestWarning();
            var command = new WarningReportCommand(warning);
            commandDispatcher.Submit(command);
            //Assert.Equal(testContext.Warnings.Count > 0, true);
        }

        private void InitializeContext()
        {
            // nothing here yet.
        }

        private BuildWarning CreateTestWarning()
        {
            var proj = new Project()
            {
                Id = 112,
                Name = "TransVault"
            };
            BuildWarning warning = new BuildWarning()
            {
                Build = new Build(proj.Id)
                {
                    Id = 101,
                    BuildDate = DateTime.Now,
                    BuildNumber = 44,
                    LogFileName = @"c:\tmp\log.log",
                    Project = proj
                },
                CodeLineNumber = 56,
                Id = 1024,
                SourceFileName = @"\sources\code.cs",
                WarningType = "CS102"
            };
            return warning;
        }
    }
}
