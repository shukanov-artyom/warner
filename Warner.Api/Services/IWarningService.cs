using System;
using System.Collections.Generic;
using Warner.Domain;

namespace Warner.Api.Services
{
    public interface IWarningService
    {
        IEnumerable<BuildWarning> GetForBuild(long buildId);

        IDictionary<string, int> GetSummaryForBuild(long buildId);

        BuildWarning SaveNew(BuildWarning warning);

        IEnumerable<BuildWarning> SaveNew(List<BuildWarning> warnings);
    }
}
