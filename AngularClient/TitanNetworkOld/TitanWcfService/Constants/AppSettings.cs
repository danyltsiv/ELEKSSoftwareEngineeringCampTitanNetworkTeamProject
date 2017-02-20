using System.Configuration;

namespace TitanWcfService.Constants
{
    public class AppSettings: PatternsTemplates.Singleton<AppSettings>
    {
       private AppSettings(){}

       public readonly string UserDataFilePath = ConfigurationManager.AppSettings["usersFilePath"];
       public readonly string FriendsDataFilePath = ConfigurationManager.AppSettings["friendsFilePath"];
       public readonly string NotFoundURL = ConfigurationManager.AppSettings["NotFoundURL"];

    }
}