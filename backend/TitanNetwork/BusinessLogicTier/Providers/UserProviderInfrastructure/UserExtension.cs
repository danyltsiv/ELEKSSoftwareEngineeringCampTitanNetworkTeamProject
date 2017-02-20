using BusinessLogicTier.DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTier.Providers
{
    public abstract class UserExtension : BaseProvider
    {
        /*private const string DllFilePath = @"DatabaseExtension.dll";
        protected delegate string Callback(string id);
        protected Callback callback;

        [DllImport(DllFilePath, CallingConvention = CallingConvention.StdCall)]
        protected static extern void RegisterCallback(Callback callback);

        [DllImport(DllFilePath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        protected static extern string GetWayBetweenUsers(string id1, string id2);

        [DllImport(DllFilePath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        protected static extern string GetCommonFriends(string id1, string id2);*/
        
    
        public abstract IQueryable<User> GetMutualFriends(User user1, User user2);
        public abstract IQueryable<User> GetWayBetweenFriends(User user1, User user2);
    }
}