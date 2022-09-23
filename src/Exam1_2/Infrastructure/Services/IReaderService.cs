using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IReaderService
    {
        void CreateReader(Reader reader);

    }
}