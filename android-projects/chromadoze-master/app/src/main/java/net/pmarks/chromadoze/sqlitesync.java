package net.pmarks.chromadoze;

/**
 * Created by tejasvi on 26/12/15.
 */
public class sqlitesync {

    String _serialNo;
    String _patientID;
    String _macID;
    String _isActive;
    String _hashValue;
    String _logDate;
    String _logTime;
    String _Duration;
    String _isSync;

    public sqlitesync(){

    }

    public sqlitesync( String serialNo, String patientID, String macID){
        this._macID = macID;
        this._serialNo = serialNo;
        this._patientID= patientID;
    }

    public sqlitesync( String serialNo,  String macID){
        this._macID = macID;
        this._serialNo = serialNo;
    }

    public sqlitesync( String HashValue, String serialNo, String patientID, String macID){
        this._macID = macID;
        this._serialNo = serialNo;
        this._patientID= patientID;
        this._hashValue= HashValue;
    }

    public sqlitesync( String isActive){
        this._isActive = isActive;
    }

    public sqlitesync(String logDate, String LogTime, String Duration, String isActive, String isSync){
        this._logDate=logDate;
        this._logTime=LogTime;
        this._Duration=Duration;
        this._isActive=isActive;
        this._isSync=isSync;
    }

    public String get_logDate(){return this._logDate;}
    public void set_logDate(String Logdate){
        this._logDate=Logdate;
    }

    public String get_logTime(){return this._logTime;}
    public void set_logTime(String LogTime){
        this._logTime=LogTime;
    }

    public String get_Duration(){return this._Duration;}
    public void set_Duration(String Duration){
        this._Duration=Duration;
    }

    public String get_isSync(){return this._isSync;}
    public void set_isSync(String isSync){
        this._isSync=isSync;
    }


    public String getPatientID(){
        return this._patientID;
    }

    public void setPatientID(String patientID){

        this._patientID=patientID;
    }

    public String getSerialno(){
        return this._serialNo;
    }

    public void setSerialNo(String serialNo){

        this._serialNo=serialNo;
    }

    public String getmacID(){
        return this._macID;
    }

    public void setmacID(String macID){

        this._macID=macID;
    }

    public String getisActive(){
        return this._isActive;
    }

    public void setisActive(String isActive){

        this._isActive=isActive;
    }

    public String gethashValue(){
        return this._hashValue;
    }

    public void sethashValue(String hashValue){

        this._hashValue=hashValue;
    }

}
