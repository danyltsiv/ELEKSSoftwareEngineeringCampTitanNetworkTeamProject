using System.Runtime.Serialization;

namespace WCFService.DataTransferObjects
{
    /// <summary>
    /// Result of operation in WCF endpoint
    /// </summary>
    [DataContract]
    public class OperationResultDTO
    {

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public object Info { get; set; }
    }
}