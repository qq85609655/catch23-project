﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.269
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码由 wsdl 自动生成, Version=4.0.30319.1。
// 
namespace WebService {
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="qqOnlineWebServiceSoap", Namespace="http://WebXml.com.cn/")]
    public partial class qqOnlineWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback qqCheckOnlineOperationCompleted;
        
        /// <remarks/>
        public qqOnlineWebService() {
            string urlSetting = System.Configuration.ConfigurationManager.AppSettings["EndpointURL"];
            if ((urlSetting != null)) {
                this.Url = urlSetting;
            }
            else {
                this.Url = "http://www.webxml.com.cn/webservices/qqOnlineWebService.asmx";
            }
        }
        
        /// <remarks/>
        public event qqCheckOnlineCompletedEventHandler qqCheckOnlineCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/qqCheckOnline", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string qqCheckOnline(string qqCode) {
            object[] results = this.Invoke("qqCheckOnline", new object[] {
                        qqCode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginqqCheckOnline(string qqCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("qqCheckOnline", new object[] {
                        qqCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndqqCheckOnline(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void qqCheckOnlineAsync(string qqCode) {
            this.qqCheckOnlineAsync(qqCode, null);
        }
        
        /// <remarks/>
        public void qqCheckOnlineAsync(string qqCode, object userState) {
            if ((this.qqCheckOnlineOperationCompleted == null)) {
                this.qqCheckOnlineOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqqCheckOnlineOperationCompleted);
            }
            this.InvokeAsync("qqCheckOnline", new object[] {
                        qqCode}, this.qqCheckOnlineOperationCompleted, userState);
        }
        
        private void OnqqCheckOnlineOperationCompleted(object arg) {
            if ((this.qqCheckOnlineCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.qqCheckOnlineCompleted(this, new qqCheckOnlineCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void qqCheckOnlineCompletedEventHandler(object sender, qqCheckOnlineCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class qqCheckOnlineCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal qqCheckOnlineCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}