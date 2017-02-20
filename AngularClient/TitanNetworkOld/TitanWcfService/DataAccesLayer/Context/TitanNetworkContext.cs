using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TitanWcfService.DataAccesLayer.Context
{
    class TitanNetworkContext : DbContext
    {
        public TitanNetworkContext()
            : base("name=TitanNetwork")
        {
            Database.SetInitializer(new TitanNetworkInitializer());
        }

        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Message> Messages { get; set; }
        public DbSet<Entities.Chat> Chats { get; set; }
        public DbSet<Entities.Room> Rooms { get; set; }
        public DbSet<Entities.BotCache> BotCaches { get; set; }
        public DbSet<Entities.MessageLog> MessageLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
