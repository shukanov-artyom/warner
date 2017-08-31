using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Reportage.DomainServices
{
    public interface IWarningService
    {
        List<BuildWarning> AllForBuild(long buildId);

        IDictionary<string, int> GetSummaryForBuild(long buildId);

        IDictionary<string, int> GetMovementsForBuild(long buildId);
    }
}
