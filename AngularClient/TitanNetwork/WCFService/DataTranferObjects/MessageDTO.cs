using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BusinessLogicTier.DataTranferObjects
{
    [DataContract]
    public class MessageDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string NewContent { get; set; }

        [DataMember]
        public DateTime SendDate;
    }
}