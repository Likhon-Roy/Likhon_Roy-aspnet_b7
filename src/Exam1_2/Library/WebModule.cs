using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Library
{
    public class WebModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            // builder.RegisterType<CourseCreateModel>().AsSelf();


            base.Load(builder);
        }
    }
}
