using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFService.DataTranferObjects
{
    /// <summary>
    /// Message content
    /// </summary>
    [DataContract]
    public class MessageDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string NewContent { get; set; }
        
        [DataMember]
        public int UserId { get; set; }
    }
}