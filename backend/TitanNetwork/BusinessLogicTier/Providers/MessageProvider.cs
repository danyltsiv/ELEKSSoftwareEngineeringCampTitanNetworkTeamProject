using BusinessLogicTier.DataAccesLayer.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using TitanWcfService.Services.Parsers;

namespace BusinessLogicTier.Providers
{
    /// <summary>
    /// Class MessageProvider.
    /// </summary>
    /// <seealso cref="BusinessLogicTier.Providers.BaseProvider" />
    public class MessageProvider : BaseProvider
    {
        /// <summary>
        /// Add new message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message id or -1 otherwise</returns>
        public int AddMessage(Message message)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.AddMessage");
                ParseMessage(message);
                Context.Entry(message).State = EntityState.Added;
                Context.SaveChanges();
                return Context.Messages.ToList().Last().Id;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.AddMessage - " + ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Remove message from db
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>bool</returns>
        public bool RemoveMessage(Message message)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.RemoveMessage");
                Context.Entry(message).State = EntityState.Deleted;
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.RemoveMessage - " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Update this message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>bool</returns>
        public bool UpdateMessage(Message message)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.UpdateMessage");
                ParseMessage(message);
                Context.Entry(message).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.UpdateMessage - " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Get concrete message
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Message</returns>
        public Message GetMessageById(int id)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.GetMessageById");
                return Context.Messages.First(g => g.Id == id);
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.GetMessageById - " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Parses the message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMessage(Message message)
        {
            var urlParser = new URLParser();
            var emailParser = new EMailParser();
            var mathParser = new MathParser();
            message.NewContent = mathParser.Parse(emailParser.Parse(urlParser.Parse(message.NewContent)));
        }
    }
}
