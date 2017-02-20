using System.Collections.Generic;

namespace TitanWcfService.DataAccesLayer.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public int Age { get; set; }
        public string About { get; set; }

        public ICollection<Room> Rooms { get; set; }       
    }
}