using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Library.Areas.Admin.Models;

namespace Library
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookCreateModel>().AsSelf();
            builder.RegisterType<ReaderCreateModel>().AsSelf();
            builder.RegisterType<BookListModel>().AsSelf();
            builder.RegisterType<ReaderListModel>().AsSelf();

            base.Load(builder);
        }
    }
}
