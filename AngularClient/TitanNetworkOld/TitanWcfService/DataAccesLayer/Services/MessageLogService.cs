namespace TeamProject.DataAccesLayer.Services
{
    public class MessageLogService
    {
        private TitanWcfService.DataAccesLayer.Context.TitanNetworkContext context;

        public MessageLogService()
        {
            context = new TitanWcfService.DataAccesLayer.Context.TitanNetworkContext();
        }

        public void AddMessageLog(TitanWcfService.DataAccesLayer.Entities.MessageLog log)
        {
            context.MessageLogs.Add(log);
        }
    }
}
