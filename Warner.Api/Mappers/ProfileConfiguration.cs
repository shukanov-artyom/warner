using AutoMapper;
using Warner.Domain;
using Warner.Persistency.Entities;

namespace Warner.Api.Mappers
{
    public class ProfileConfiguration : Profile
    {
        public ProfileConfiguration()
        {
            Bidirectional<ProjectEntity, Project>();
            Bidirectional<BuildEntity, Build>();
            Bidirectional<BuildWarningEntity, BuildWarning>();
        }

        private void Bidirectional<TSource, TTarget>()
        {
            CreateMap<TSource, TTarget>();
            CreateMap<TTarget, TSource>();
        }
    }
}
