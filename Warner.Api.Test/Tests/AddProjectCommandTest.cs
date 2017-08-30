using System;
using Warner.Api.Commands.AddProject;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;
using Xunit;

namespace Warner.Api.Test.Tests
{
    public class AddProjectCommandTest : CqrsUnitTestClass
    {
        [Fact]
        public void Test()
        {
            var proj = new Project()
            {
                Id = 0,
                Name = "TestProject"
            };
            var command = new AddProjectCommand(proj);
            ICommandResult commandResult = Execute(command);

            Assert.Equal(commandResult.IsSuccess, true);
        }
    }
}
