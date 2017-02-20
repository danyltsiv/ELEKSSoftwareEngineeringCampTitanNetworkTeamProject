using System.Runtime.Serialization;

namespace BusinessLogicTier.DataTranferObjects
{
    [DataContract]
    public class ChatDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}