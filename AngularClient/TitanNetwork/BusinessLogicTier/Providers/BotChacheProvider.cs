using System.Linq;
using BusinessLogicTier.DataAccesLayer.Context;
using BusinessLogicTier.DataAccesLayer.Entities;
using System.Data.Entity;

namespace BusinessLogicTier.Providers
{
    public class BotCacheProvider
    {
        public BotCacheProvider()
        {
        }

        public bool AddBotCache(BotCache cache)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(cache).State = EntityState.Added;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public BotCache GetBotCacheExistance(string title)
        {
            BotCache cacheFound;

            using (var context = new TitanNetworkContext())
            {
                cacheFound = context.BotCaches.FirstOrDefault(g => g.Title == title);
            }

            return cacheFound;
        }
    }
}
