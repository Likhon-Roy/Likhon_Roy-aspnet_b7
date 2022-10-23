using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public abstract class BaseMyORM<G, T> where T : class, new()
    {
        public abstract Task Insert(T item);
        public abstract Task Update(T item);
        public abstract Task Delete(T item);
        public abstract Task Delete(G id);
        public abstract Task GetById(G id);
        public abstract Task GetAll();
    }
}