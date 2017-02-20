using BusinessLogicTier.DataAccesLayer.Context;
using BusinessLogicTier.DataAccesLayer.Entities;
using System.Data.Entity;
using System.Linq;

namespace BusinessLogicTier.Providers
{
    public class MessageProvider
    {
        public MessageProvider()
        {
        }

        public bool AddMessage(Message message)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(message).State = EntityState.Added;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMessage(Message message)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(message).State = EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateMessage(Message message)
        {
            try
            {
                using (var context = new TitanNetworkContext())
                {
                    context.Entry(message).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Message GetMessageById(int id)
        {
            Message msg;

            using (var context = new TitanNetworkContext())
            {
                msg = context.Messages.FirstOrDefault(g=>g.Id == id);
            }

            return msg;
        }
    }
}
