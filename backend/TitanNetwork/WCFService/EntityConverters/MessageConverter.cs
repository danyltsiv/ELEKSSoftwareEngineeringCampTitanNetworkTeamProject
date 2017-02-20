using System.Collections.Generic;
using BusinessLogicTier.DataAccesLayer.Entities;
using WCFService.EntityConverters;
using WCFService.DataTranferObjects;
using System;

namespace WCFService.EntityConverters
{
    /// <summary>
    /// Class MessageConverter.
    /// </summary>
    /// <seealso cref="WCFService.EntityConverters.IEntityConverter{BusinessLogicTier.DataAccesLayer.Entities.Message, WCFService.DataTranferObjects.MessageDTO}" />
    public class MessageConverter : IEntityConverter<Message,MessageDTO>
    {
        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TBusinessEntity.</returns>
        public Message ToBusinessEntity(MessageDTO model)
        {
            var message = new Message();
            message.Id = model.Id;
            message.NewContent = model.NewContent;
            message.OldContent = model.NewContent;
            message.SendDate = DateTime.Now;
            message.UserId = model.UserId;
            return message;
        }

        /// <summary>
        /// To the business entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TBusinessEntity&gt;.</returns>
        public IList<Message> ToBusinessEntityList(IList<MessageDTO> models)
        {
            var list = new List<Message>();
            foreach (var model in models)
                list.Add(ToBusinessEntity(model));
            return list;
        }

        /// <summary>
        /// To the data transfer entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TDataTransferEntity.</returns>
        public MessageDTO ToDataTransferEntity(Message model)
        {
            var message = new MessageDTO();
            message.Id = model.Id;
            message.NewContent = model.NewContent;
            message.UserId = model.UserId;
            return message;
        }

        /// <summary>
        /// To the data transfer entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TDataTransferEntity&gt;.</returns>
        public IList<MessageDTO> ToDataTransferEntityList(IList<Message> models)
        {
            var list = new List<MessageDTO>();
            foreach (var model in models)
                list.Add(ToDataTransferEntity(model));
            return list;
        }
    }
}