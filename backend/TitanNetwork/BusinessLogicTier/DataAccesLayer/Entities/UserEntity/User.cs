using BusinessLogicTier.Authentication;
using System.Collections.Generic;

namespace BusinessLogicTier.DataAccesLayer.Entities
{
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public string About { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}