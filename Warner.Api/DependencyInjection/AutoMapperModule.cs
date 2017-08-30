using Autofac;
using AutoMapper;

namespace Warner.Api.DependencyInjection
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IConfigurationProvider provider = Mapper.Configuration;
            builder.RegisterInstance<IConfigurationProvider>(provider);
            builder.Register<IMapper>(sp =>
                new Mapper(sp.Resolve<IConfigurationProvider>(), sp.Resolve));
        }
    }
}
