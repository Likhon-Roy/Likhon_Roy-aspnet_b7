using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Library.Areas.Admin.Models;

namespace Library
{
    public class WebModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookCreateModel>().AsSelf();
            builder.RegisterType<ReaderCreateModel>().AsSelf();

            base.Load(builder);
        }
    }
}
