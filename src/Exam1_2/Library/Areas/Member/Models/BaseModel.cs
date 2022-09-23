using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Library.Areas.Member.Models
{
    public class BaseModel
    {
        protected ILifetimeScope _scope;
        // public ITimeService _timeService;

        public BaseModel()
        {

        }

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            // _timeService = _scope.Resolve<ITimeService>();
        }
    }
}
