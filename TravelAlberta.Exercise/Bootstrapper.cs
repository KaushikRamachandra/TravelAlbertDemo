using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using TravelAlberta.Exercise.Domain.Parser;
using TravelAlberta.Exercise.Services;

namespace TravelAlberta.Exercise
{
    public static class Bootstrapper
    {
        public static IContainer Run(ApplicationConfigService configService)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(configService).As<IVersionInfo>().SingleInstance();
            builder.RegisterInstance(configService).As<IConfigInfo>().SingleInstance();
            builder.RegisterInstance(configService).As<IServerInfo>().SingleInstance();

            builder.RegisterGeneric(typeof(DomainMapper<>)).As(typeof(IDomainMapper<>)).InstancePerDependency();
            builder.RegisterType<CsvParser>().AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AsImplementedInterfaces();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Set the WebApi dependency resolver.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}