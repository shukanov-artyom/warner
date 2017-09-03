using System;
using System.Collections.Generic;
using Warner.Domain;
using Warner.Reportage.ViewModels.Factories;
using Xunit;

namespace Warner.Reportage.Test
{
    public class MovementFactoryOnWarningsTest
    {
        [Fact]
        public void Test()
        {
            List<BuildWarning> current = GetCurrentWarnings();
            List<BuildWarning> previous = GetPreviousWarnings();
            var factory = new MovementFactoryOnWarnings(current, previous);
            var result = factory.Create();
            Assert.Equal(4, result.Count);
        }

        private List<BuildWarning> GetCurrentWarnings()
        {
            return new List<BuildWarning>()
            {
                new BuildWarning()
                {
                    WarningType = "cs10",
                    SourceFileName = "file1.cs",
                    CodeLineNumber = 33,
                    BuildId = 1,
                    LogLineNumber = 100
                },
                new BuildWarning()
                {
                    WarningType = "cs10",
                    SourceFileName = "file1.cs",
                    CodeLineNumber = 44,
                    BuildId = 1,
                    LogLineNumber = 101
                },
                new BuildWarning()
                {
                    WarningType = "cs11",
                    SourceFileName = "file2.cs",
                    CodeLineNumber = 33,
                    BuildId = 1,
                    LogLineNumber = 102
                },
                new BuildWarning()
                {
                    WarningType = "cs11",
                    SourceFileName = "file2.cs",
                    CodeLineNumber = 34,
                    BuildId = 1,
                    LogLineNumber = 103
                }
            };
        }

        private List<BuildWarning> GetPreviousWarnings()
        {
            return new List<BuildWarning>()
            {
                new BuildWarning()
                {
                    // this one will disappear
                    WarningType = "cs22",
                    SourceFileName = "file1.cs",
                    CodeLineNumber = 37,
                    BuildId = 1,
                    LogLineNumber = 100
                },
                new BuildWarning()
                {
                    // this one will remain
                    WarningType = "cs10",
                    SourceFileName = "file1.cs",
                    CodeLineNumber = 33,
                    BuildId = 1,
                    LogLineNumber = 100
                },
                new BuildWarning()
                {
                    // this one will remain too
                    WarningType = "cs10",
                    SourceFileName = "file1.cs",
                    CodeLineNumber = 44,
                    BuildId = 1,
                    LogLineNumber = 101
                },
                // these will appear
//                new BuildWarning()
//                {
//                    WarningType = "cs11",
//                    SourceFileName = "file2.cs",
//                    CodeLineNumber = 33,
//                    BuildId = 1,
//                    LogLineNumber = 102
//                },
//                new BuildWarning()
//                {
//                    WarningType = "cs11",
//                    SourceFileName = "file2.cs",
//                    CodeLineNumber = 34,
//                    BuildId = 1,
//                    LogLineNumber = 103
//                }
            };
        }
    }
}
