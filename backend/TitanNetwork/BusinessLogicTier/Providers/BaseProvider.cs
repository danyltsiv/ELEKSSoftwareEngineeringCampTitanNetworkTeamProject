using BusinessLogicTier.DataAccesLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTier.Providers
{
    public class BaseProvider : IDisposable
    {
        protected TitanNetworkContext Context { get; private set; }

        public BaseProvider()
        {
            Context = new TitanNetworkContext();
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }
    }
}
