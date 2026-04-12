using System;
using System.IO;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using TrioBO;
using TrioBAL;

namespace TinnitusTrioService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
         //private static ILog logger LogManager.GetLogger(typeof(Service1));
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool GetUniqueId(string uId)
        {
            try
            {
                Log.Info("Inside  GetUniqueId");
                var uniqueId = new TrioBusinessObject {UniqueId = (uId)};
                
                Log.Info("Unique ID: " + uId);
                var triouniqueid = new TrioData();
                var trioRetVal = triouniqueid.GetUniqueId(uniqueId);
                Log.Info("Trio RetVal : " + trioRetVal);
                return trioRetVal;
            }
            catch (Exception ex)
            {
                //TrioBAL.MailerLog.SendLogDetails("Please Note. The patient with the following ID " + testObj.uniqueid + " with the hash tag " + testObj.hashid + " has successfully activated the system. If not, please de activate the system.");
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in GetUniqueID Method " + ex.ToString());
                throw;
            }
        }

        public bool GetUniqueId1()
        {
            try
            {
                Log.Info("Inside  GetUniqueId1");
                var uniqueId = new TrioBusinessObject();
                var triouniqueid = new TrioData();
                var trioRetVal = triouniqueid.GetUniqueId1();
                Log.Info("Trio RetVal : " + trioRetVal);
                return trioRetVal;
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in GetUniqueID1 Method " + ex.ToString());
                throw;
            }
        }

        //public bool InsertPatientDetails(TrioUseridObject csvPatientDetails)
        public bool insertpatient(Stream JsonArray)
       // public bool insertpatient(string uniqueid, string macid, string serialno, string hashid)
        {
            try
            {
                Log.Info("Inside  insertpatient");
                var objJson = new StreamReader(JsonArray);

                var obj = objJson.ReadToEnd();

                var testObj = new JavaScriptSerializer().Deserialize<stringarray>(obj);
                var trioInsertpatientDetails = new TrioData();
                var trioRetVal = trioInsertpatientDetails.InsertPatientDetails(testObj.uniqueid, testObj.macid, testObj.serialno, testObj.hashid);
                Log.Info("Trio RetVal : " + trioRetVal);
                //var trioRetVal = trioInsertpatientDetails.InsertPatientDetails(uniqueid, macid, serialno, hashid);
                TrioBAL.MailerLog.SendLogDetails("Please Note. The patient with the following ID " + testObj.uniqueid + " with the hash tag " + testObj.hashid + " has successfully activated the system. If not, please de activate the system.");
                return trioRetVal;

                // return testObj.serialno;
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in insertpatient Method " + ex.ToString());
                throw;
            }
        }

        public bool checkactivestatus(Stream JsonArray)
       
        {
            try
            {
                Log.Info("Inside  checkactivestatus");
                var objJson = new StreamReader(JsonArray);

                var obj = objJson.ReadToEnd();

                var testObj = new JavaScriptSerializer().Deserialize<stringarray>(obj);
                var trioInsertpatientDetails = new TrioData();
                var trioRetVal = trioInsertpatientDetails.CheckActiveStatus(testObj.uniqueid);
                Log.Info("Trio RetVal : " + trioRetVal);
                return trioRetVal;

                // return testObj.serialno;
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in checkactivestatus Method " + ex.ToString());
                throw;
            }
        }

        //sync methods here
        public string synccmtdata(Stream xmlfile)
        {

           // var xmlString = ConvertJsonToXml(xmlfile);
            Log.Info("Inside  synccmtdata");
            var retVal = "";

            try
            {
                
                var trioInsertpatientDetails = new TrioData();

                StreamReader reader = new StreamReader(xmlfile);
                var JSONdata = reader.ReadToEnd();

                var xmlFile= JSONdata = JSONdata.Replace(@"\", "");

              var xmlf=  ConvertJsonToXml(Regex.Unescape(xmlFile));

                var finalxmlf = Regex.Unescape(xmlf);

                var trioRetVal = trioInsertpatientDetails.SyncLogFiles(finalxmlf, "CMT");
                switch (trioRetVal)
                {
                    case ("Success"):
                       return retVal = "true";
                        break;
                    case ("Fail"):
                       return  retVal = "false";
                        break;
                }

                if (xmlfile == null)
                   return retVal = "invalidxml";

                return finalxmlf;

                //if (JSONdata == null)
                //{
                //    return false;
                //}

                //else
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in synccmtdata Method " + ex.ToString());
                return ex.ToString();
            }
        }

        public string SyncCmmData(Stream xmlfile)
        {
            Log.Info("Inside  SyncCmmData");
            var retVal = "";

            try
            {

                var trioInsertpatientDetails = new TrioData();

                StreamReader reader = new StreamReader(xmlfile);
                var JSONdata = reader.ReadToEnd();



                var xmlFile = JSONdata = JSONdata.Replace(@"\", "");

                var xmlf = ConvertJsonToXml(Regex.Unescape(xmlFile));

                var finalxmlf = Regex.Unescape(xmlf);

                var trioRetVal = trioInsertpatientDetails.SyncLogFiles(finalxmlf, "CMM");
                switch (trioRetVal)
                {
                    case ("Success"):
                        return retVal = "true";
                        break;
                    case ("Fail"):
                        return retVal = "false";
                        break;
                }

                if (xmlfile == null)
                    return retVal = "invalidxml";

                return finalxmlf;

                //if (JSONdata == null)
                //{
                //    return false;
                //}

                //else
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in synccmmdata Method " + ex.ToString());
                return ex.ToString();
            }
        }

        public string SyncCmesData(Stream xmlfile)
        {
            Log.Info("Inside  SyncCmesData");
            var retVal = "";

            try
            {

                var trioInsertpatientDetails = new TrioData();

                StreamReader reader = new StreamReader(xmlfile);
                var JSONdata = reader.ReadToEnd();



                var xmlFile = JSONdata = JSONdata.Replace(@"\", "");

                var xmlf = ConvertJsonToXml(Regex.Unescape(xmlFile));

                var finalxmlf = Regex.Unescape(xmlf);

                var trioRetVal = trioInsertpatientDetails.SyncLogFiles(finalxmlf, "CMES");
                switch (trioRetVal)
                {
                    case ("Success"):
                        return retVal = "true";
                        break;
                    case ("Fail"):
                        return retVal = "false";
                        break;
                }

                if (xmlfile == null)
                    return retVal = "invalidxml";

                return finalxmlf;

                //if (JSONdata == null)
                //{
                //    return false;
                //}

                //else
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in synccmesdata Method " + ex.ToString());
                return ex.ToString();
            }
        }

        public string SyncCmnData(Stream xmlfile)
        {
            Log.Info("Inside  SyncCmnData");
            var retVal = "";

            try
            {

                var trioInsertpatientDetails = new TrioData();

                StreamReader reader = new StreamReader(xmlfile);
                var JSONdata = reader.ReadToEnd();



                var xmlFile = JSONdata = JSONdata.Replace(@"\", "");

                var xmlf = ConvertJsonToXml(Regex.Unescape(xmlFile));

                var finalxmlf = Regex.Unescape(xmlf);

                var trioRetVal = trioInsertpatientDetails.SyncLogFiles(finalxmlf, "CMN");
                switch (trioRetVal)
                {
                    case ("Success"):
                        return retVal = "true";
                        break;
                    case ("Fail"):
                        return retVal = "false";
                        break;
                }

                if (xmlfile == null)
                    return retVal = "invalidxml";

                return finalxmlf;

                //if (JSONdata == null)
                //{
                //    return false;
                //}

                //else
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in synccmndata Method " + ex.ToString());
                return ex.ToString();
            }
        }

        public bool CheckPatientCreds(string csvPatientDetails)
        {
            try
            {
                Log.Info("Inside  CheckPatientCreds");
                var trioInsertpatientDetails = new TrioData();
                var trioRetVal = trioInsertpatientDetails.CheckPatientCreds(csvPatientDetails);
                Log.Info("Trio RetVal : " + trioRetVal);
                return trioRetVal;
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in checkpatientcreds Method " + ex.ToString());
                throw;
            }
        }

        public string ConvertJsonToXml(string jsonValue)
        {
            Log.Info("Inside  ConvertJsonToXml");
            var contents = jsonValue;

            //var contents = File.ReadAllText(@"E:\\t.txt");

            contents = @"{
   '?xml': {
     '@version': '1.0',
     '@standalone': 'no'
   },
   'root': {
'person':" + contents + @"}
}";

            try
            {
                var doc = JsonConvert.DeserializeXmlNode(contents);
                return doc.InnerXml.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("Error : " + ex);
                TrioBAL.MailerLog.SendErrorDetails("Error in convertjsontoxml Method " + ex.ToString());
                throw;
            }
        }
    }

    public class stringarray
    {
        public string uniqueid { get; set; }
        public string macid { get; set; }

        public string serialno { get; set; }
        public string hashid { get; set; }
    }
}
