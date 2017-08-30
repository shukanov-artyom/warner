using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warner.Domain;
using Warner.Persistency;
using Warner.Persistency.Entities;

namespace Warner.Api.Services
{
    public class WarningService : IWarningService
    {
        private readonly ApplicationDataContext databaseContext;
        private readonly IMapper mapper;

        public WarningService(
            DbContext databaseContext,
            IMapper mapper)
        {
            this.databaseContext = databaseContext as ApplicationDataContext
                ?? throw new ArgumentNullException(nameof(databaseContext));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<BuildWarning> GetForBuild(long buildId)
        {
            IEnumerable<BuildWarningEntity> entities =
                databaseContext.Warnings.Where(w => w.Build.Id == buildId);
            foreach (BuildWarningEntity entity in entities)
            {
                yield return mapper.Map<BuildWarning>(entity);
            }
        }

        public IDictionary<string, int> GetSummaryForBuild(long buildId)
        {
            IDictionary<string, int> result = new Dictionary<string, int>();
            var distinctWarningTypes = databaseContext.Warnings
                .Where(w => w.BuildId == buildId)
                .Select(w => w.WarningType)
                .Distinct()
                .ToList();
            foreach (var type in distinctWarningTypes)
            {
                int count = databaseContext.Warnings
                    .Count(w => w.BuildId == buildId && w.WarningType == type);
                result[type] = count;
            }
            return result;
            // EF Core does not support OrderBy!
//            return databaseContext.Warnings
//                .Where(w => w.BuildId == buildId)
//                .GroupBy(w => w.WarningType)
//                .OrderByDescending(c => c.Count())
//                .Select(c => new { c.Key, Count = c.Count() })
//                .ToDictionary(k => k.Key, v => v.Count);
        }

        public BuildWarning SaveNew(BuildWarning warning)
        {
            BuildWarningEntity entity = mapper.Map<BuildWarningEntity>(warning);
            databaseContext.Warnings.Add(entity);
            databaseContext.SaveChanges();
            return mapper.Map<BuildWarning>(entity);
        }

        public IEnumerable<BuildWarning> SaveNew(List<BuildWarning> warnings)
        {
            foreach (BuildWarning buildWarning in warnings)
            {
                yield return SaveNew(buildWarning);
            }
        }
    }
}
