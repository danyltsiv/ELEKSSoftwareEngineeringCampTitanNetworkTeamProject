using System.Collections.Generic;
using BusinessLogicTier.DataAccesLayer.Entities;
using WCFService.DataTranferObjects;

namespace WCFService.EntityConverters
{
    /// <summary>
    /// Class ChatConverter.
    /// </summary>
    /// <seealso cref="WCFService.EntityConverters.IEntityConverter{BusinessLogicTier.DataAccesLayer.Entities.Chat, WCFService.DataTranferObjects.ChatDTO}" />
    public class ChatConverter : IEntityConverter<Chat, ChatDTO>
    {
        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TBusinessEntity.</returns>
        public Chat ToBusinessEntity(ChatDTO model)
        {
            var chat = new Chat();
            chat.Id = model.Id;
            chat.Title = model.Title;
            return chat;
        }

        /// <summary>
        /// To the business entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TBusinessEntity&gt;.</returns>
        public IList<Chat> ToBusinessEntityList(IList<ChatDTO> models)
        {
            var list = new List<Chat>();
            foreach (var model in models)
                list.Add(ToBusinessEntity(model));
            return list;
        }

        /// <summary>
        /// To the data transfer entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TDataTransferEntity.</returns>
        public ChatDTO ToDataTransferEntity(Chat model)
        {
            var chat = new ChatDTO();
            chat.Id = model.Id;
            chat.Title = model.Title;
            return chat;
        }

        /// <summary>
        /// To the data transfer entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TDataTransferEntity&gt;.</returns>
        public IList<ChatDTO> ToDataTransferEntityList(IList<Chat> models)
        {
            var list = new List<ChatDTO>();
            foreach (var model in models)
                list.Add(ToDataTransferEntity(model));
            return list;
        }
    }
}