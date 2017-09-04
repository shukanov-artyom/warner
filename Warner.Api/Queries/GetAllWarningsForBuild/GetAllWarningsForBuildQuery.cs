using System;
using System.Collections;
using System.Collections.Generic;
using Warner.Api.Cqrs.Infrastructure;
using Warner.Domain;

namespace Warner.Api.Queries.GetAllWarningsForBuild
{
    public class GetAllWarningsForBuildQuery : IQuery
    {
        private readonly string warningType;
        private readonly long buildId;

        public GetAllWarningsForBuildQuery(long buildId)
        {
            this.buildId = buildId;
        }

        public GetAllWarningsForBuildQuery(
            long buildId,
            string warningType)
        {
            this.buildId = buildId;
            this.warningType = warningType;
        }

        public IQueryResult Run(IQueryContext context)
        {
            GetAllWarningsForBuildQueryContext typedContext =
                context as GetAllWarningsForBuildQueryContext;
            IEnumerable<BuildWarning> result;
            if (String.IsNullOrEmpty(warningType))
            {
                result = typedContext.Warnings.GetForBuild(buildId);
            }
            else
            {
                result = typedContext.Warnings.GetOfTypeForBuild(buildId, warningType);
            }
            return new GetAllWarningsForBuildQueryResult(result);
        }
    }
}
