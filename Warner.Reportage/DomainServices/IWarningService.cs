using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Reportage.DomainServices
{
    public interface IWarningService
    {
        List<BuildWarning> AllForBuild(long buildId);

        List<BuildWarning> AllForBuildOfType(long buildId, string warningType);

        IDictionary<string, int> GetSummaryForBuild(long buildId);
    }
}
