namespace TitanWcfService.DataAccesLayer.Services
{
    public class MessageService
    {
        private TitanWcfService.DataAccesLayer.Context.TitanNetworkContext context;

        public MessageService()
        {
            context = new TitanWcfService.DataAccesLayer.Context.TitanNetworkContext();
        }

        public void AddMessage(TitanWcfService.DataAccesLayer.Entities.Message message)
        {
            context.Messages.Add(message);
        }

        public void RemoveMessage(TitanWcfService.DataAccesLayer.Entities.Message message)
        {
            context.Messages.Remove(message);
        }
    }
}
