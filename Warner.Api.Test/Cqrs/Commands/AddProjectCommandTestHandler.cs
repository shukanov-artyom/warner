using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Warner.Api.Commands.AddProject;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Api.Services;
using Warner.Domain;

namespace Warner.Api.Test.Cqrs.Commands
{
    internal class AddProjectCommandTestHandler :
        ITestCommandHandler<AddProjectCommand>
    {
        private readonly IList<Project> proj = new List<Project>();

        public ICommandResult Handle(AddProjectCommand command)
        {
            AddProjectCommandContext commandContext = AssembleMockCommandContext();
            return command.Execute(commandContext);
        }

        private AddProjectCommandContext AssembleMockCommandContext()
        {
            var projectService = new Mock<IProjectService>();
            var unitOfWork = new Mock<IUnitOfWork>();

            SetupProjectServiceMock(projectService);

            SetupUnitOfWorkMock(unitOfWork);
            var result = new AddProjectCommandContext(
                new Mock<IProjectService>().Object,
                new Mock<IUnitOfWork>().Object);
            return result;
        }

        private void SetupProjectServiceMock(Mock<IProjectService> mock)
        {
            mock.Setup(service => service.SaveNew(It.IsAny<Project>()))
                .Returns((Project p) =>
                {
                    p.Id = 11;
                    proj.Add(p);
                    return p;
                });
            mock.Setup(service => service.GetByName(It.IsAny<string>()))
                .Returns((string s) => proj.First(p => p.Name == s));
        }

        private void SetupUnitOfWorkMock(Mock<IUnitOfWork> mock)
        {
            // nothing here yet
            // mock.Setup(m => m.SubmitChanges()
        }
    }
}
