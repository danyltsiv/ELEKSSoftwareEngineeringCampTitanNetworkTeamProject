using System.Collections.Generic;

namespace TitanWcfService.DataAccesLayer.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
