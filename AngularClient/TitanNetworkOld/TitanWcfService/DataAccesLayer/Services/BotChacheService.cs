using System.Linq;



namespace TitanWcfService.DataAccesLayer.Services
{
    public class BotChacheService
    {
        private TitanWcfService.DataAccesLayer.Context.TitanNetworkContext context;

        public BotChacheService()
        {
            context = new TitanWcfService.DataAccesLayer.Context.TitanNetworkContext();
        }

        public void AddBotCache(TitanWcfService.DataAccesLayer.Entities.BotCache cache)
        {
            context.BotCaches.Add(cache);
        }

        public TitanWcfService.DataAccesLayer.Entities.BotCache GetBotCacheExistance(string title)
        {
            TitanWcfService.DataAccesLayer.Entities.BotCache cacheFound = context.BotCaches.FirstOrDefault(g => g.Title == title);

            return cacheFound;
        }
    }
}
