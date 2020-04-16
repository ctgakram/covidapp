﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppProj.Service.EmailServiceReferance {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.imail.isoap.com/", ConfigurationName="EmailServiceReferance.EmailWS")]
    public interface EmailWS {
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.imail.isoap.com/EmailWS/sendEmailRequest", ReplyAction="http://ws.imail.isoap.com/EmailWS/sendEmailResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        AppProj.Service.EmailServiceReferance.sendEmailResponse sendEmail(AppProj.Service.EmailServiceReferance.sendEmailRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.imail.isoap.com/EmailWS/sendEmailRequest", ReplyAction="http://ws.imail.isoap.com/EmailWS/sendEmailResponse")]
        System.Threading.Tasks.Task<AppProj.Service.EmailServiceReferance.sendEmailResponse> sendEmailAsync(AppProj.Service.EmailServiceReferance.sendEmailRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.imail.isoap.com/")]
    public partial class jobs : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string appUserIdField;
        
        private attachment[] attachmentsField;
        
        private string bccAddressField;
        
        private string bodyField;
        
        private string captionField;
        
        private string ccAddressField;
        
        private bool completeField;
        
        private System.DateTime feedbackDateField;
        
        private bool feedbackDateFieldSpecified;
        
        private string feedbackEmailField;
        
        private string feedbackNameField;
        
        private bool feedbackSentField;
        
        private string fromAddressField;
        
        private string fromTextField;
        
        private string gatewayField;
        
        private string jobContentTypeField;
        
        private long jobIdField;
        
        private bool jobIdFieldSpecified;
        
        private jobRecipients[] jobRecipientsField;
        
        private string modeField;
        
        private int numberOfItemField;
        
        private bool numberOfItemFieldSpecified;
        
        private int numberOfItemFailedField;
        
        private bool numberOfItemFailedFieldSpecified;
        
        private int numberOfItemSentField;
        
        private bool numberOfItemSentFieldSpecified;
        
        private string priorityField;
        
        private string requesterField;
        
        private string statusField;
        
        private string subjectField;
        
        private string toAddressField;
        
        private string toTextField;
        
        private string udValue1Field;
        
        private string udValue2Field;
        
        private string udValue3Field;
        
        private string udValue4Field;
        
        private string udValue5Field;
        
        private string udValue6Field;
        
        private string udValue7Field;
        
        private string vtemplateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string appUserId {
            get {
                return this.appUserIdField;
            }
            set {
                this.appUserIdField = value;
                this.RaisePropertyChanged("appUserId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attachments", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public attachment[] attachments {
            get {
                return this.attachmentsField;
            }
            set {
                this.attachmentsField = value;
                this.RaisePropertyChanged("attachments");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string bccAddress {
            get {
                return this.bccAddressField;
            }
            set {
                this.bccAddressField = value;
                this.RaisePropertyChanged("bccAddress");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string body {
            get {
                return this.bodyField;
            }
            set {
                this.bodyField = value;
                this.RaisePropertyChanged("body");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string caption {
            get {
                return this.captionField;
            }
            set {
                this.captionField = value;
                this.RaisePropertyChanged("caption");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string ccAddress {
            get {
                return this.ccAddressField;
            }
            set {
                this.ccAddressField = value;
                this.RaisePropertyChanged("ccAddress");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public bool complete {
            get {
                return this.completeField;
            }
            set {
                this.completeField = value;
                this.RaisePropertyChanged("complete");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public System.DateTime feedbackDate {
            get {
                return this.feedbackDateField;
            }
            set {
                this.feedbackDateField = value;
                this.RaisePropertyChanged("feedbackDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool feedbackDateSpecified {
            get {
                return this.feedbackDateFieldSpecified;
            }
            set {
                this.feedbackDateFieldSpecified = value;
                this.RaisePropertyChanged("feedbackDateSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string feedbackEmail {
            get {
                return this.feedbackEmailField;
            }
            set {
                this.feedbackEmailField = value;
                this.RaisePropertyChanged("feedbackEmail");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string feedbackName {
            get {
                return this.feedbackNameField;
            }
            set {
                this.feedbackNameField = value;
                this.RaisePropertyChanged("feedbackName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=10)]
        public bool feedbackSent {
            get {
                return this.feedbackSentField;
            }
            set {
                this.feedbackSentField = value;
                this.RaisePropertyChanged("feedbackSent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=11)]
        public string fromAddress {
            get {
                return this.fromAddressField;
            }
            set {
                this.fromAddressField = value;
                this.RaisePropertyChanged("fromAddress");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=12)]
        public string fromText {
            get {
                return this.fromTextField;
            }
            set {
                this.fromTextField = value;
                this.RaisePropertyChanged("fromText");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=13)]
        public string gateway {
            get {
                return this.gatewayField;
            }
            set {
                this.gatewayField = value;
                this.RaisePropertyChanged("gateway");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=14)]
        public string jobContentType {
            get {
                return this.jobContentTypeField;
            }
            set {
                this.jobContentTypeField = value;
                this.RaisePropertyChanged("jobContentType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=15)]
        public long jobId {
            get {
                return this.jobIdField;
            }
            set {
                this.jobIdField = value;
                this.RaisePropertyChanged("jobId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool jobIdSpecified {
            get {
                return this.jobIdFieldSpecified;
            }
            set {
                this.jobIdFieldSpecified = value;
                this.RaisePropertyChanged("jobIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("jobRecipients", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=16)]
        public jobRecipients[] jobRecipients {
            get {
                return this.jobRecipientsField;
            }
            set {
                this.jobRecipientsField = value;
                this.RaisePropertyChanged("jobRecipients");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=17)]
        public string mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
                this.RaisePropertyChanged("mode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=18)]
        public int numberOfItem {
            get {
                return this.numberOfItemField;
            }
            set {
                this.numberOfItemField = value;
                this.RaisePropertyChanged("numberOfItem");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool numberOfItemSpecified {
            get {
                return this.numberOfItemFieldSpecified;
            }
            set {
                this.numberOfItemFieldSpecified = value;
                this.RaisePropertyChanged("numberOfItemSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=19)]
        public int numberOfItemFailed {
            get {
                return this.numberOfItemFailedField;
            }
            set {
                this.numberOfItemFailedField = value;
                this.RaisePropertyChanged("numberOfItemFailed");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool numberOfItemFailedSpecified {
            get {
                return this.numberOfItemFailedFieldSpecified;
            }
            set {
                this.numberOfItemFailedFieldSpecified = value;
                this.RaisePropertyChanged("numberOfItemFailedSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=20)]
        public int numberOfItemSent {
            get {
                return this.numberOfItemSentField;
            }
            set {
                this.numberOfItemSentField = value;
                this.RaisePropertyChanged("numberOfItemSent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool numberOfItemSentSpecified {
            get {
                return this.numberOfItemSentFieldSpecified;
            }
            set {
                this.numberOfItemSentFieldSpecified = value;
                this.RaisePropertyChanged("numberOfItemSentSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=21)]
        public string priority {
            get {
                return this.priorityField;
            }
            set {
                this.priorityField = value;
                this.RaisePropertyChanged("priority");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=22)]
        public string requester {
            get {
                return this.requesterField;
            }
            set {
                this.requesterField = value;
                this.RaisePropertyChanged("requester");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=23)]
        public string status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=24)]
        public string subject {
            get {
                return this.subjectField;
            }
            set {
                this.subjectField = value;
                this.RaisePropertyChanged("subject");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=25)]
        public string toAddress {
            get {
                return this.toAddressField;
            }
            set {
                this.toAddressField = value;
                this.RaisePropertyChanged("toAddress");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=26)]
        public string toText {
            get {
                return this.toTextField;
            }
            set {
                this.toTextField = value;
                this.RaisePropertyChanged("toText");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=27)]
        public string udValue1 {
            get {
                return this.udValue1Field;
            }
            set {
                this.udValue1Field = value;
                this.RaisePropertyChanged("udValue1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=28)]
        public string udValue2 {
            get {
                return this.udValue2Field;
            }
            set {
                this.udValue2Field = value;
                this.RaisePropertyChanged("udValue2");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=29)]
        public string udValue3 {
            get {
                return this.udValue3Field;
            }
            set {
                this.udValue3Field = value;
                this.RaisePropertyChanged("udValue3");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=30)]
        public string udValue4 {
            get {
                return this.udValue4Field;
            }
            set {
                this.udValue4Field = value;
                this.RaisePropertyChanged("udValue4");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=31)]
        public string udValue5 {
            get {
                return this.udValue5Field;
            }
            set {
                this.udValue5Field = value;
                this.RaisePropertyChanged("udValue5");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=32)]
        public string udValue6 {
            get {
                return this.udValue6Field;
            }
            set {
                this.udValue6Field = value;
                this.RaisePropertyChanged("udValue6");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=33)]
        public string udValue7 {
            get {
                return this.udValue7Field;
            }
            set {
                this.udValue7Field = value;
                this.RaisePropertyChanged("udValue7");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=34)]
        public string vtemplate {
            get {
                return this.vtemplateField;
            }
            set {
                this.vtemplateField = value;
                this.RaisePropertyChanged("vtemplate");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.imail.isoap.com/")]
    public partial class attachment : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string extensionField;
        
        private byte[] fileField;
        
        private string fileNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string extension {
            get {
                return this.extensionField;
            }
            set {
                this.extensionField = value;
                this.RaisePropertyChanged("extension");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", Order=1)]
        public byte[] file {
            get {
                return this.fileField;
            }
            set {
                this.fileField = value;
                this.RaisePropertyChanged("file");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("fileName");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.imail.isoap.com/")]
    public partial class responseMessage : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codeField;
        
        private long jobIdField;
        
        private string messageField;
        
        private int statusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("code");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public long jobId {
            get {
                return this.jobIdField;
            }
            set {
                this.jobIdField = value;
                this.RaisePropertyChanged("jobId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public int status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.imail.isoap.com/")]
    public partial class jobRecipients : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int failCountField;
        
        private bool failCountFieldSpecified;
        
        private byte[] imageField;
        
        private long jobDetailIdField;
        
        private bool jobDetailIdFieldSpecified;
        
        private string recipientEmailField;
        
        private string recipientTypeField;
        
        private bool sentField;
        
        private System.DateTime sentDateField;
        
        private bool sentDateFieldSpecified;
        
        private string toTextField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public int failCount {
            get {
                return this.failCountField;
            }
            set {
                this.failCountField = value;
                this.RaisePropertyChanged("failCount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool failCountSpecified {
            get {
                return this.failCountFieldSpecified;
            }
            set {
                this.failCountFieldSpecified = value;
                this.RaisePropertyChanged("failCountSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", Order=1)]
        public byte[] image {
            get {
                return this.imageField;
            }
            set {
                this.imageField = value;
                this.RaisePropertyChanged("image");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public long jobDetailId {
            get {
                return this.jobDetailIdField;
            }
            set {
                this.jobDetailIdField = value;
                this.RaisePropertyChanged("jobDetailId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool jobDetailIdSpecified {
            get {
                return this.jobDetailIdFieldSpecified;
            }
            set {
                this.jobDetailIdFieldSpecified = value;
                this.RaisePropertyChanged("jobDetailIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string recipientEmail {
            get {
                return this.recipientEmailField;
            }
            set {
                this.recipientEmailField = value;
                this.RaisePropertyChanged("recipientEmail");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string recipientType {
            get {
                return this.recipientTypeField;
            }
            set {
                this.recipientTypeField = value;
                this.RaisePropertyChanged("recipientType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public bool sent {
            get {
                return this.sentField;
            }
            set {
                this.sentField = value;
                this.RaisePropertyChanged("sent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public System.DateTime sentDate {
            get {
                return this.sentDateField;
            }
            set {
                this.sentDateField = value;
                this.RaisePropertyChanged("sentDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sentDateSpecified {
            get {
                return this.sentDateFieldSpecified;
            }
            set {
                this.sentDateFieldSpecified = value;
                this.RaisePropertyChanged("sentDateSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string toText {
            get {
                return this.toTextField;
            }
            set {
                this.toTextField = value;
                this.RaisePropertyChanged("toText");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendEmail", WrapperNamespace="http://ws.imail.isoap.com/", IsWrapped=true)]
    public partial class sendEmailRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.imail.isoap.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AppProj.Service.EmailServiceReferance.jobs jobs;
        
        public sendEmailRequest() {
        }
        
        public sendEmailRequest(AppProj.Service.EmailServiceReferance.jobs jobs) {
            this.jobs = jobs;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendEmailResponse", WrapperNamespace="http://ws.imail.isoap.com/", IsWrapped=true)]
    public partial class sendEmailResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.imail.isoap.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AppProj.Service.EmailServiceReferance.responseMessage @return;
        
        public sendEmailResponse() {
        }
        
        public sendEmailResponse(AppProj.Service.EmailServiceReferance.responseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface EmailWSChannel : AppProj.Service.EmailServiceReferance.EmailWS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmailWSClient : System.ServiceModel.ClientBase<AppProj.Service.EmailServiceReferance.EmailWS>, AppProj.Service.EmailServiceReferance.EmailWS {
        
        public EmailWSClient() {
        }
        
        public EmailWSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmailWSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailWSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailWSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppProj.Service.EmailServiceReferance.sendEmailResponse AppProj.Service.EmailServiceReferance.EmailWS.sendEmail(AppProj.Service.EmailServiceReferance.sendEmailRequest request) {
            return base.Channel.sendEmail(request);
        }
        
        public AppProj.Service.EmailServiceReferance.responseMessage sendEmail(AppProj.Service.EmailServiceReferance.jobs jobs) {
            AppProj.Service.EmailServiceReferance.sendEmailRequest inValue = new AppProj.Service.EmailServiceReferance.sendEmailRequest();
            inValue.jobs = jobs;
            AppProj.Service.EmailServiceReferance.sendEmailResponse retVal = ((AppProj.Service.EmailServiceReferance.EmailWS)(this)).sendEmail(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AppProj.Service.EmailServiceReferance.sendEmailResponse> AppProj.Service.EmailServiceReferance.EmailWS.sendEmailAsync(AppProj.Service.EmailServiceReferance.sendEmailRequest request) {
            return base.Channel.sendEmailAsync(request);
        }
        
        public System.Threading.Tasks.Task<AppProj.Service.EmailServiceReferance.sendEmailResponse> sendEmailAsync(AppProj.Service.EmailServiceReferance.jobs jobs) {
            AppProj.Service.EmailServiceReferance.sendEmailRequest inValue = new AppProj.Service.EmailServiceReferance.sendEmailRequest();
            inValue.jobs = jobs;
            return ((AppProj.Service.EmailServiceReferance.EmailWS)(this)).sendEmailAsync(inValue);
        }
    }
}
