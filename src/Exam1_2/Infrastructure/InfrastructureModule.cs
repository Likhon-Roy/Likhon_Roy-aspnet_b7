using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWorks;

namespace Infrastructure
{
    public class InfrastructureModule : Module
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

            builder.RegisterType<BookService>().As<IBookService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookRepository>().As<IBookRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReaderService>().As<IReaderService>()
            .InstancePerLifetimeScope();

            builder.RegisterType<ReaderRepository>().As<IReaderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TimeService>().As<ITimeService>()
            .InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
