using System.Linq;
using BusinessLogicTier.DataAccesLayer.Entities;
using System.Data.Entity;
using TitanWcfService.Services.Bots.Parsers;
using TitanWcfService.Services.Bots.Managers;
using TitanWcfService.Services.Bots.Responses;
using System.Collections.Generic;
using BusinessLogicTier.Providers.UserProviderInfrastructure;
using BusinessLogicTier.Providers.Models;
using System;

namespace BusinessLogicTier.Providers
{
    public class BotServiceProvider : BaseProvider
    {
        private static string BotLogin = "titanbot";
        private static string BotChatTitle = "TitanBot chat";

        /// <summary>
        /// Return unique ID for this bot
        /// </summary>
        /// <returns>ID</returns>
        public int GetBotUserId()
        {
            using (var provider = new UserProvider())
            {
                return provider.FindByLogin(BotLogin).Id;
            }
        }

        /// <summary>
        /// Attach current bot to user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>OperationResult</returns>
        public OperationResult AttachBotToUser(int userId)
        {
            int createdChatId = UserHasChatWithBot(userId);
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.AttachBotToUser");
                
                using (var chatProvider = new ChatProvider())
                {
                    if (createdChatId != 0)
                        return new OperationResult()
                        {
                            Result = true,
                            Info = createdChatId
                        };

                    // Create chat and add it to db
                    createdChatId = chatProvider.AddChat(new Chat()
                    {
                        Id = 0,
                        Title = BotChatTitle
                    });

                    var chat = new Chat() { Id = createdChatId };

                    // Add current user to this chat
                    chatProvider.AddUserToChat(new User() { Id = userId }, chat);

                    // Add bot to this chat
                    chatProvider.AddUserToChat(new User() { Id = GetBotUserId() }, chat);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.AttachBotToUser - " + ex.ToString());
                return new OperationResult()
                {
                    Result = false,
                    Info = ex.ToString()
                };
            }

            return new OperationResult()
            {
                Result = true,
                Info = createdChatId
            };
        }

        /// <summary>
        /// Add message, that will be analysed by the bot
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chatId"></param>
        /// <returns>OperationResult</returns>
        public OperationResult SendMessageToBot(Message message, int chatId)
        {
            try
            {
                Logger.log.Debug("at BusinessLogicTier.Providers.SendMessageToBot - " + message.NewContent);
                var result = GetResponseFromBot(message.NewContent);
                using (var messageProvider = new MessageProvider())
                {
                    var botResponse = new Message()
                    {
                        NewContent = result,
                        ChatId = chatId,
                        UserId = GetBotUserId()
                    };
                    Logger.log.Debug("Bot response is: " + botResponse);
                    messageProvider.AddMessage(botResponse);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error("at BusinessLogicTier.Providers.SendMessageToBot");
                return new OperationResult()
                {
                    Result = false,
                    Info = ex.ToString()
                };
            }

            return new OperationResult()
            {
                Result = true,
                Info = ""
            };
        }

        /// <summary>
        /// Analyse message and get response from bot
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Response from bot</returns>
        public string GetResponseFromBot(string message)
        {
            Logger.log.Debug("at BusinessLogicTier.Providers.GetREsponseFromBot");
            var commandParser = new CommandParser();
            var manager = new CommandManager();
            var data = commandParser.ParseMessage(message);

            var response = new Responser(commandParser.Commands, commandParser.ExceptionCommands);
            response.CreateMessage(commandParser.SplittedMessage);

            var listWithAnswers = new List<string>();

            foreach (var temp in data)
            {
                foreach (var arg in temp.Value)
                {
                    var result = manager.Execute(temp.Key, arg);

                    if (commandParser.ExceptionCommands.Contains(temp.Key))
                    {
                        response.SetResultInsteadCommand(temp.Key, result);
                    }
                    else
                    {
                        listWithAnswers.Add(result);
                    }
                }
            }

            var wcm = new WithoutCommandManager(commandParser.ExceptionCommands);
            var data2 = wcm.FindExpressions(response.Response);
            foreach (var temp in data2)
            {
                foreach (var arg in temp.Value)
                {
                    var result = manager.Execute(temp.Key, arg);
                    response.SetResultInsteadWithoutCommand(arg, result);
                }
            }

            var returnResult = listWithAnswers.Aggregate((a, b) => a + "\n\n" + b) + "\n\n" + response.Response;
            return returnResult;
        }

        private int UserHasChatWithBot(int userId)
        {
            using (var provider = new UserProvider())
            {
                var chat = provider.GetChatsOfUser(new User() { Id = userId }).ToList().Find(ch => ch.Title == BotChatTitle);
                return chat == null ? 0 : chat.Id;
            }
        }
    }
}
