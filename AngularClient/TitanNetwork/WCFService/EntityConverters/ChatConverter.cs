using System.Collections.Generic;
using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.EntityConverters;
using BusinessLogicTier.DataTranferObjects;

namespace WCFService.EntityConverters
{
    public class ChatConverter : IEntityConverter<Chat, ChatDTO>
    {
        public Chat ToBusinessEntity(ChatDTO model)
        {
            var chat = new Chat();
            chat.Id = model.Id;
            chat.Title = model.Title;
            return chat;
        }

        public IList<Chat> ToBusinessEntityList(IList<ChatDTO> models)
        {
            var list = new List<Chat>();
            foreach (var model in models)
                list.Add(ToBusinessEntity(model));
            return list;
        }

        public ChatDTO ToDataTransferEntity(Chat model)
        {
            var chat = new ChatDTO();
            chat.Id = model.Id;
            chat.Title = model.Title;
            return chat;
        }

        public IList<ChatDTO> ToDataTransferEntityList(IList<Chat> models)
        {
            var list = new List<ChatDTO>();
            foreach (var model in models)
                list.Add(ToDataTransferEntity(model));
            return list;
        }
    }
}