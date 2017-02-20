using BusinessLogicTier.DataAccesLayer.Context;
using BusinessLogicTier.DataAccesLayer.Entities;
using System.Data.Entity;

namespace BusinessLogicTier.Providers
{
    public class MessageLogProvider
    {
        public MessageLogProvider()
        {
        }

        public bool AddMessageLog(MessageLog log)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(log).State = EntityState.Added;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
