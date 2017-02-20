using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicTier.Providers;
using BusinessLogicTier.Providers.UserProviderInfrastructure;

namespace BusinessLogicTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserProvider usrPrv = new UserProvider();
            //var user = usrPrv.GetUserById(1);
            //var user2 = usrPrv.GetUserById(5);
            //var way = usrPrv.GetWayBetweenFriends(user, user2);
            //Console.WriteLine("{0},{1},{2}",way[0].FirstName,
            //                    way[1].FirstName,
            //                    way[2].FirstName);
            ChatProvider c = new ChatProvider();
            UserProvider u = new UserProvider();
            var user = u.GetUserById(2);
            var chat = c.GetChatById(3);
            c.AddUserToChat(user, chat);
            Console.ReadLine();
        }
    }
}
