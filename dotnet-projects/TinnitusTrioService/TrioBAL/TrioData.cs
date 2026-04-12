using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TrioBO;
using TrioDAL;
using log4net;

namespace TrioBAL
{
    public class TrioData
    {
        private ClsGetUniqueId _trioUniqueId = null;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool GetUniqueId(TrioBusinessObject objTrioBusinessObject)
        {
            try
            {
                _trioUniqueId= new ClsGetUniqueId();
                var triouniqueidDs = _trioUniqueId.GetUniqueId(objTrioBusinessObject);
                Log.Info("Unique ID count: " + triouniqueidDs.Tables[0].Rows.Count);
                return triouniqueidDs.Tables[0].Rows.Count != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetUniqueId1()
        {
            try
            {
                _trioUniqueId = new ClsGetUniqueId();
                var triouniqueidDs = _trioUniqueId.GetUniqueId1();
                Log.Info("Unique ID 1 count: " + triouniqueidDs.Tables[0].Rows.Count);
                return triouniqueidDs.Tables[0].Rows.Count != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertPatientDetails(string uniqueid, string macid, string serialno, string hashid)
        {
            try
            {
                var objTrioUserObject = new TrioUseridObject
                {
                    UniqueId = (uniqueid),
                    Macid = macid,
                    SerialNo = serialno,
                    UniqueString = hashid
                };

                _trioUniqueId = new ClsGetUniqueId();
                var triouniqueidDs = _trioUniqueId.InsertPatientDetails(objTrioUserObject);

                Log.Info("InsertPatientDetails BAL: " + triouniqueidDs);
                return triouniqueidDs =="SUCCESS";
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Log sync method
        public string SyncLogFiles(string xmlString, string modulename)
        {
            try
            {
                _trioUniqueId = new ClsGetUniqueId();
                var returnString = _trioUniqueId.SyncLogFiles(xmlString, modulename);

                return returnString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //check active status of the user
        public bool CheckActiveStatus(string patientid)
        {
            try
            {
                _trioUniqueId = new ClsGetUniqueId();
                var returnString = _trioUniqueId.CheckActiveStatus(patientid);


                return returnString.Equals("TRUE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckPatientCreds(string csvPatientDetails)
        {
            try
            {
                //split comma separated values here
                var cmmaSepval = csvPatientDetails.Split(',')
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

                //map each array value to the object string
                var objTrioUserObject = new TrioUseridObject
                {
                    UniqueId =(cmmaSepval[0]),
                    Macid = cmmaSepval[1],
                    SerialNo = cmmaSepval[2],
                    UniqueString = cmmaSepval[3]
                };

                _trioUniqueId = new ClsGetUniqueId();
                var triouniqueidDs = _trioUniqueId.CheckPatientPhoneCreds(objTrioUserObject);

                Log.Info("CheckPatientCreds BAL: " + triouniqueidDs);
                return triouniqueidDs == "SUCCESS";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
