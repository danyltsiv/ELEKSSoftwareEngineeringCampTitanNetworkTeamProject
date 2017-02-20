using System;

namespace TitanWcfService.DataAccesLayer.Entities
{
    public class MessageLog : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }

        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
    }
}
