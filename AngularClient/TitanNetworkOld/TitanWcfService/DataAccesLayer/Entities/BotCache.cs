using System;

namespace TitanWcfService.DataAccesLayer.Entities
{
    public class BotCache : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
    }
}
