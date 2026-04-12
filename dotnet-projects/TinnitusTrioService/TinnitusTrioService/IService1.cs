using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TrioBO;

namespace TinnitusTrioService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "com.androidhive.musicplayer")]
    public interface IService1
    {

        // TODO: Add your service operations here

        [OperationContract]
        bool GetUniqueId(string uId);

        [OperationContract]
        [WebGet(
            UriTemplate = "getunique",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool GetUniqueId1();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "insertpatient",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool insertpatient(Stream JsonArray);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "checkactivestatus",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool checkactivestatus(Stream JsonArray);


        [OperationContract]
        bool CheckPatientCreds(string csvPatientDetails);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "synccmtdata",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Xml)]
        [OperationContract]
        string synccmtdata(Stream xmlfile);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "synccmmdata",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string SyncCmmData(Stream xmlfile);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "synccmesdata",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string SyncCmesData(Stream xmlfile);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "synccmndata",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string SyncCmnData(Stream xmlfile);

        [OperationContract]
        string ConvertJsonToXml(string jsonValue);
    }
}
