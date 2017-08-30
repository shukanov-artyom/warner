using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Api.Test.Cqrs
{
    /// <summary>
    /// in-memory data context.
    /// </summary>
    public class TestDataContext
    {
        public TestDataContext()
        {
            Projects = new List<Project>();
            Builds = new List<Build>();
            Warnings = new List<BuildWarning>();
        }

        public IList<Project> Projects { get; }

        public IList<Build> Builds { get; }

        public IList<BuildWarning> Warnings { get; }
    }
}
