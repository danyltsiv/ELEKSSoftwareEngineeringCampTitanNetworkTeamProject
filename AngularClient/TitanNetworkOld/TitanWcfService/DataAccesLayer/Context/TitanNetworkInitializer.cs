using System.Collections.Generic;
using System.Data.Entity;

namespace TitanWcfService.DataAccesLayer.Context
{
    class TitanNetworkInitializer : DropCreateDatabaseAlways<TitanNetworkContext>
    {
        protected override void Seed(TitanNetworkContext context)
        {
            var users = new List<Entities.User>();
            users.Add(new Entities.User()
            {
                FirstName = "John",
                MidleName = "Dick",
                LastName = "Doe",
                Login = "darkstalker97",
                Password = "1234",
                Age = 18,
                About = "Play dota all day"
            });

            users.Add(new Entities.User()
            {
                FirstName = "Xavier",
                MidleName = "Gradle",
                LastName = "Jinjer",
                Login = "lolipop",
                Password = "qwerty",
                Age = 22,
                About = "Basketball, guitar"
            });

            users.Add(new Entities.User()
            {
                FirstName = "Pro100",
                MidleName = "Vasya",
                LastName = "Pupkin",
                Login = "vovo4ka23",
                Password = "weorgew",
                Age = 11,
                About = "Go to wkola"
            });

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
