using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.DbContexts;

namespace Infrastructure
{
    public class InfrastructureModule: Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            // builder.RegisterType<CourseService>().As<ICourseService>()
            //     .InstancePerLifetimeScope();

            // builder.RegisterType<CourseRepository>().As<ICourseRepository>()
            //     .InstancePerLifetimeScope();

            // builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            //     .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
