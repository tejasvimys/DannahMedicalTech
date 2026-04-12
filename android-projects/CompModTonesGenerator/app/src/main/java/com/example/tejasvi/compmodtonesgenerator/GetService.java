package com.example.tejasvi.compmodtonesgenerator;

import android.util.Log;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONStringer;

/**
 * Created by tejasvi on 3/1/16.
 */
public class GetService  {

    private final static String Service_Uri = "http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc";

    public boolean InsertUserDetails(String UserId, String MacID, String SerialNo, String UniqueId) {

        try {

            HttpPost request = new HttpPost("http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc/insertpatient");
            request.setHeader("Accept", "application/json");
            request.setHeader("Content-type", "application/json, charset=utf-8");

            JSONStringer userIDS = new JSONStringer()
                    .object()
                    .key("uniqueid").value((UserId))
                    .key("macid").value(MacID)
                    .key("serialno").value("AAEE123456")
                    .key("hashid").value(UniqueId)
                    .endObject();

            StringEntity entity = new StringEntity(userIDS.toString());

            request.setEntity(entity);

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpResponse response = httpClient.execute(request);

            String responseText = EntityUtils.toString(response.getEntity());

            boolean responsvalue=false;

            if (responseText.equals("true"))
            {
                 responsvalue=true;
            }

            else {
                responsvalue=false;
            }

            Log.d("Web Invoke", "Saving User Details" + response.getStatusLine().getStatusCode());


            return responsvalue;
        }


        catch (Exception ex){
            ex.printStackTrace();
            Log.d("Web Invoke", "Error: " + ex);
            return false;
        }

}

    public String InsertLogDetails(JSONArray JsonValue){

        try {

            HttpPost request = new HttpPost("http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc/synccmtdata");
            request.setHeader("Accept", "application/json");
            request.setHeader("Content-type", "application/json, charset=utf-8");

            JSONStringer userIDS = new JSONStringer()
                    .object()
                    .key("uniqueid").value((JsonValue))

                    .endObject();

            StringEntity entity = new StringEntity(JsonValue.toString());
            request.setEntity(entity);

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpResponse response = httpClient.execute(request);

            String responseText = EntityUtils.toString(response.getEntity());

            return responseText;
        }

        catch (Exception ex){
            ex.printStackTrace();
            Log.d("Web Invoke", "Error: " + ex);
            return "False";
        }
    }

    public boolean DeactivateStatus(String UserId){

        try {

            HttpPost request = new HttpPost("http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc/checkactivestatus");
            request.setHeader("Accept", "application/json");
            request.setHeader("Content-type", "application/json, charset=utf-8");

            JSONStringer userIDS = new JSONStringer()
                    .object()
                    .key("uniqueid").value((UserId))

                    .endObject();
            StringEntity entity = new StringEntity(userIDS.toString());
            request.setEntity(entity);

            DefaultHttpClient httpClient = new DefaultHttpClient();
            HttpResponse response = httpClient.execute(request);

            String responseText = EntityUtils.toString(response.getEntity());

            boolean responsvalue=false;

            if (responseText.equals("true"))
            {
                responsvalue=true;
            }

            else {
                responsvalue=false;
            }

            return responsvalue;
        }

        catch (Exception ex){
            ex.printStackTrace();
            Log.d("Web Invoke", "Error: " + ex);
            return true;
        }
    }

}
