﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogicTier.Tests.UserServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserInfoDTO", Namespace="http://schemas.datacontract.org/2004/07/WCFService.DataTranferObjects")]
    [System.SerializableAttribute()]
    public partial class UserInfoDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AboutField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AgeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MidleNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string About {
            get {
                return this.AboutField;
            }
            set {
                if ((object.ReferenceEquals(this.AboutField, value) != true)) {
                    this.AboutField = value;
                    this.RaisePropertyChanged("About");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Age {
            get {
                return this.AgeField;
            }
            set {
                if ((this.AgeField.Equals(value) != true)) {
                    this.AgeField = value;
                    this.RaisePropertyChanged("Age");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MidleName {
            get {
                return this.MidleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MidleNameField, value) != true)) {
                    this.MidleNameField = value;
                    this.RaisePropertyChanged("MidleName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ChatDTO", Namespace="http://schemas.datacontract.org/2004/07/WCFService.DataTranferObjects")]
    [System.SerializableAttribute()]
    public partial class ChatDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllUsers", ReplyAction="http://tempuri.org/IUserService/GetAllUsersResponse")]
        BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllUsers", ReplyAction="http://tempuri.org/IUserService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserById", ReplyAction="http://tempuri.org/IUserService/GetUserByIdResponse")]
        BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO GetUserById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserById", ReplyAction="http://tempuri.org/IUserService/GetUserByIdResponse")]
        System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO> GetUserByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetChatsOfUser", ReplyAction="http://tempuri.org/IUserService/GetChatsOfUserResponse")]
        BusinessLogicTier.Tests.UserServiceReference.ChatDTO[] GetChatsOfUser(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetChatsOfUser", ReplyAction="http://tempuri.org/IUserService/GetChatsOfUserResponse")]
        System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.ChatDTO[]> GetChatsOfUserAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserFriends", ReplyAction="http://tempuri.org/IUserService/GetUserFriendsResponse")]
        BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetUserFriends(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserFriends", ReplyAction="http://tempuri.org/IUserService/GetUserFriendsResponse")]
        System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetUserFriendsAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetMutualFriends", ReplyAction="http://tempuri.org/IUserService/GetMutualFriendsResponse")]
        BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetMutualFriends(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo1, BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetMutualFriends", ReplyAction="http://tempuri.org/IUserService/GetMutualFriendsResponse")]
        System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetMutualFriendsAsync(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo1, BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetWayBetweenUsers", ReplyAction="http://tempuri.org/IUserService/GetWayBetweenUsersResponse")]
        BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetWayBetweenUsers(int id1, int id2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetWayBetweenUsers", ReplyAction="http://tempuri.org/IUserService/GetWayBetweenUsersResponse")]
        System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetWayBetweenUsersAsync(int id1, int id2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddUser", ReplyAction="http://tempuri.org/IUserService/AddUserResponse")]
        void AddUser(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddUser", ReplyAction="http://tempuri.org/IUserService/AddUserResponse")]
        System.Threading.Tasks.Task AddUserAsync(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUser", ReplyAction="http://tempuri.org/IUserService/UpdateUserResponse")]
        void UpdateUser(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO newUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUser", ReplyAction="http://tempuri.org/IUserService/UpdateUserResponse")]
        System.Threading.Tasks.Task UpdateUserAsync(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO newUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUserById", ReplyAction="http://tempuri.org/IUserService/DeleteUserByIdResponse")]
        void DeleteUserById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUserById", ReplyAction="http://tempuri.org/IUserService/DeleteUserByIdResponse")]
        System.Threading.Tasks.Task DeleteUserByIdAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : BusinessLogicTier.Tests.UserServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<BusinessLogicTier.Tests.UserServiceReference.IUserService>, BusinessLogicTier.Tests.UserServiceReference.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO GetUserById(int id) {
            return base.Channel.GetUserById(id);
        }
        
        public System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO> GetUserByIdAsync(int id) {
            return base.Channel.GetUserByIdAsync(id);
        }
        
        public BusinessLogicTier.Tests.UserServiceReference.ChatDTO[] GetChatsOfUser(int id) {
            return base.Channel.GetChatsOfUser(id);
        }
        
        public System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.ChatDTO[]> GetChatsOfUserAsync(int id) {
            return base.Channel.GetChatsOfUserAsync(id);
        }
        
        public BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetUserFriends(int id) {
            return base.Channel.GetUserFriends(id);
        }
        
        public System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetUserFriendsAsync(int id) {
            return base.Channel.GetUserFriendsAsync(id);
        }
        
        public BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetMutualFriends(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo1, BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo2) {
            return base.Channel.GetMutualFriends(userInfo1, userInfo2);
        }
        
        public System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetMutualFriendsAsync(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo1, BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO userInfo2) {
            return base.Channel.GetMutualFriendsAsync(userInfo1, userInfo2);
        }
        
        public BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[] GetWayBetweenUsers(int id1, int id2) {
            return base.Channel.GetWayBetweenUsers(id1, id2);
        }
        
        public System.Threading.Tasks.Task<BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO[]> GetWayBetweenUsersAsync(int id1, int id2) {
            return base.Channel.GetWayBetweenUsersAsync(id1, id2);
        }
        
        public void AddUser(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO user) {
            base.Channel.AddUser(user);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO user) {
            return base.Channel.AddUserAsync(user);
        }
        
        public void UpdateUser(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO newUser) {
            base.Channel.UpdateUser(newUser);
        }
        
        public System.Threading.Tasks.Task UpdateUserAsync(BusinessLogicTier.Tests.UserServiceReference.UserInfoDTO newUser) {
            return base.Channel.UpdateUserAsync(newUser);
        }
        
        public void DeleteUserById(int id) {
            base.Channel.DeleteUserById(id);
        }
        
        public System.Threading.Tasks.Task DeleteUserByIdAsync(int id) {
            return base.Channel.DeleteUserByIdAsync(id);
        }
    }
}
