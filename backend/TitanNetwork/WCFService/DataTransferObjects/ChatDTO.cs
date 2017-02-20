using System.Runtime.Serialization;

namespace WCFService.DataTranferObjects
{
    /// <summary>
    /// Chat information
    /// </summary>
    [DataContract]
    public class ChatDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}