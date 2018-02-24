using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using ApiInfrastructure.Clients.Login;
using ApiInfrastructure.Clients.Measurement;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using DiabeticWebAppMVC5.Automapper;

namespace DiabeticWebAppMVC5
{
    public class Dependencies
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();

            builder.RegisterControllers(assemblies);

            builder.RegisterType<LoginClient>()
                .As<ILoginClient>()
                .PreserveExistingDefaults();

            builder.RegisterType<MeasurementClient>()
                .As<IMeasurementClient>()
                .PreserveExistingDefaults();

            builder.Register(c => AutomapperConfiguration.Configure()).As<IMapper>()
                .SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}