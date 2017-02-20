namespace TitanWcfService.DataAccesLayer.Entities
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
