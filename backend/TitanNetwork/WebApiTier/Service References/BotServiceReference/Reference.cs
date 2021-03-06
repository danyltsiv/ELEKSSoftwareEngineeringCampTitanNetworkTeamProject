﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiTier.BotServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageDTO", Namespace="http://schemas.datacontract.org/2004/07/WCFService.DataTranferObjects")]
    [System.SerializableAttribute()]
    public partial class MessageDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NewContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
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
        public string NewContent {
            get {
                return this.NewContentField;
            }
            set {
                if ((object.ReferenceEquals(this.NewContentField, value) != true)) {
                    this.NewContentField = value;
                    this.RaisePropertyChanged("NewContent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="OperationResultDTO", Namespace="http://schemas.datacontract.org/2004/07/WCFService.DataTransferObjects")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WebApiTier.BotServiceReference.MessageDTO))]
    public partial class OperationResultDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object InfoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ResultField;
        
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
        public object Info {
            get {
                return this.InfoField;
            }
            set {
                if ((object.ReferenceEquals(this.InfoField, value) != true)) {
                    this.InfoField = value;
                    this.RaisePropertyChanged("Info");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BotServiceReference.IBotService")]
    public interface IBotService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBotService/GetResponseFromBot", ReplyAction="http://tempuri.org/IBotService/GetResponseFromBotResponse")]
        string GetResponseFromBot(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBotService/GetResponseFromBot", ReplyAction="http://tempuri.org/IBotService/GetResponseFromBotResponse")]
        System.Threading.Tasks.Task<string> GetResponseFromBotAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBotService/SendMessageToBot", ReplyAction="http://tempuri.org/IBotService/SendMessageToBotResponse")]
        WebApiTier.BotServiceReference.OperationResultDTO SendMessageToBot(WebApiTier.BotServiceReference.MessageDTO message, int chatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBotService/SendMessageToBot", ReplyAction="http://tempuri.org/IBotService/SendMessageToBotResponse")]
        System.Threading.Tasks.Task<WebApiTier.BotServiceReference.OperationResultDTO> SendMessageToBotAsync(WebApiTier.BotServiceReference.MessageDTO message, int chatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBotService/AttachBotToUser", ReplyAction="http://tempuri.org/IBotService/AttachBotToUserResponse")]
        WebApiTier.BotServiceReference.OperationResultDTO AttachBotToUser(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBotService/AttachBotToUser", ReplyAction="http://tempuri.org/IBotService/AttachBotToUserResponse")]
        System.Threading.Tasks.Task<WebApiTier.BotServiceReference.OperationResultDTO> AttachBotToUserAsync(int userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBotServiceChannel : WebApiTier.BotServiceReference.IBotService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BotServiceClient : System.ServiceModel.ClientBase<WebApiTier.BotServiceReference.IBotService>, WebApiTier.BotServiceReference.IBotService {
        
        public BotServiceClient() {
        }
        
        public BotServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BotServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BotServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BotServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetResponseFromBot(string message) {
            return base.Channel.GetResponseFromBot(message);
        }
        
        public System.Threading.Tasks.Task<string> GetResponseFromBotAsync(string message) {
            return base.Channel.GetResponseFromBotAsync(message);
        }
        
        public WebApiTier.BotServiceReference.OperationResultDTO SendMessageToBot(WebApiTier.BotServiceReference.MessageDTO message, int chatId) {
            return base.Channel.SendMessageToBot(message, chatId);
        }
        
        public System.Threading.Tasks.Task<WebApiTier.BotServiceReference.OperationResultDTO> SendMessageToBotAsync(WebApiTier.BotServiceReference.MessageDTO message, int chatId) {
            return base.Channel.SendMessageToBotAsync(message, chatId);
        }
        
        public WebApiTier.BotServiceReference.OperationResultDTO AttachBotToUser(int userId) {
            return base.Channel.AttachBotToUser(userId);
        }
        
        public System.Threading.Tasks.Task<WebApiTier.BotServiceReference.OperationResultDTO> AttachBotToUserAsync(int userId) {
            return base.Channel.AttachBotToUserAsync(userId);
        }
    }
}
