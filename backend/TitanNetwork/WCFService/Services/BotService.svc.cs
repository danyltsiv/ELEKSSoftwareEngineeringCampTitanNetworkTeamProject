using System;
using BusinessLogicTier;
using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Providers;
using WCFService.DataTranferObjects;
using WCFService.DataTransferObjects;
using WCFService.EntityConverters;

namespace WCFService.Services
{
    public class BotService : IBotService
    {
        private BotServiceProvider _botServiceProvider;
        private MessageConverter _messageConverter;

        public BotService()
        {
            _messageConverter = new MessageConverter();
            _botServiceProvider = new BotServiceProvider();
        }

        /// <summary>
        /// Let the bot analyse this message
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Response from bot</returns>
        public string GetResponseFromBot(string message)
        {
            Logger.log.Debug("at WCFService.BotService.GetResponseFromBot");
            return _botServiceProvider.GetResponseFromBot(message);
        }

        /// <summary>
        /// Attach bot to this users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>OperationResultDTO</returns>
        public OperationResultDTO AttachBotToUser(int userId)
        {
            Logger.log.Debug("at WCFService.BotService.AttachBotToUser");
            var result = _botServiceProvider.AttachBotToUser(userId);
            return new OperationResultDTO()
            {
                Result = result.Result,
                Info = result.Info
            };
        }

        /// <summary>
        /// Let the bot analyse this message and write a response to chat
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chatId"></param>
        /// <returns>OperationResultDTO</returns>
        public OperationResultDTO SendMessageToBot(MessageDTO message, int chatId)
        {
            Logger.log.Debug("at WCFService.BotService.SendMessageToBot");
            var result = _botServiceProvider.SendMessageToBot(_messageConverter.ToBusinessEntity(message), chatId);
            return new OperationResultDTO()
            {
                Result = result.Result,
                Info = result.Info
            };
        }

        public virtual void Dispose()
        {
            _botServiceProvider.Dispose();
        }
    }
}