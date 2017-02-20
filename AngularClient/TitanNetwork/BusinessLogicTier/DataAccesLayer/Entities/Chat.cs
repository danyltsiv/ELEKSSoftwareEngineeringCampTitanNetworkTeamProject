using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTier.DataAccesLayer.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
