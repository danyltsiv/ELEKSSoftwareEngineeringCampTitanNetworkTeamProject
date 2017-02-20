using System.Runtime.Serialization;

namespace WCFService.DataTranferObjects
{
    /// <summary>
    /// Account specific information
    /// </summary>
    [DataContract]
    public class AccountDTO
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string ConfirmPassword { get; set; }

    }
}