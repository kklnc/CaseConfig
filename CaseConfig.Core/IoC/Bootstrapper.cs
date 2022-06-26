using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseConfig.Core.IoC
{
    public class Bootstrapper
    {
        public IContainer Container { get; set; }

        public virtual void DependencyResolving(IContainer instanceCurrentResolver)
        {

        }
    }
}
