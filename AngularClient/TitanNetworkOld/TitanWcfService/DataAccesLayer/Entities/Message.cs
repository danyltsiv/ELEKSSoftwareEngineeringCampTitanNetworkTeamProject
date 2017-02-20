using System;

namespace TitanWcfService.DataAccesLayer.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string OldContent { get; set; }
        public string NewContent { get; set; }
        public DateTime SendDate;

        public int UserId { get; set; }
        public int ChatId { get; set; }

        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
