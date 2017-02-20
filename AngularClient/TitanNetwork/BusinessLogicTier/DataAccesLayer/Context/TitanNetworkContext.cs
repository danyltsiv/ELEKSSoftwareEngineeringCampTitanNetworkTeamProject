using BusinessLogicTier.DataAccesLayer.Entities;
using BusinessLogicTier.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using static BusinessLogicTier.Authentication.IdentityModel;

namespace BusinessLogicTier.DataAccesLayer.Context
{
    public class TitanNetworkContext : IdentityDbContext<User, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public TitanNetworkContext()
            : base("name=TitanNetwork")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TitanNetworkContext, Configuration>());
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<BotCache> BotCaches { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}