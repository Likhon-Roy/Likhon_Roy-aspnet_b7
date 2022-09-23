using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
