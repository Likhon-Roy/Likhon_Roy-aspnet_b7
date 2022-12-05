using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace StockData.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<CourseCreateModel>().AsSelf();
            // builder.RegisterType<CourseListModel>().AsSelf();
            // builder.RegisterType<CourseEditModel>().AsSelf();
            // builder.RegisterType<RegistrationModel>().AsSelf();
            // builder.RegisterType<LoginModel>().AsSelf();


            base.Load(builder);
        }
    }
}