
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace MRO.ROI.Test
{
    public class ROIAPIHelper
    {
        public TestContext Context;

        public ROIAPIHelper(TestContext textContext)
        {
            Context = textContext;
        }
        public string CreateRequestAndChangeAPIStatusRequest()
        {
            string requestId = string.Empty;
            IRestResponse response = null;
            try
            {
                var client = new RestClient(Context.Properties["BaseURI"].ToString());
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", Context.Properties["ContentType"].ToString());
                request.AddHeader("Authorization", Context.Properties["Authorization"].ToString());

                string metaData = $"{MetaDataParams.RequesterRef}{MetaDataParams.FirstName}{MetaDataParams.LastName}{MetaDataParams.DOB}{MetaDataParams.PAN}{MetaDataParams.DOS}{MetaDataParams.DOSEND}{MetaDataParams.ROIFacilityCode}{MetaDataParams.LocationCode}{MetaDataParams.RequesterID}{MetaDataValues.RequesterRefValue}{MetaDataValues.FirstNameValue}{MetaDataValues.LastNameValue}{MetaDataValues.DOBValue}{MetaDataValues.PANValue}{MetaDataValues.DOSValue}{MetaDataValues.DOSENDValue}{MetaDataValues.ROIFacilityCodeValue}{MetaDataValues.LocationCodeValue}{MetaDataValues.RequesterIDValue}";
                request.AddParameter("MetaData", metaData);

                request.AddParameter("PDF", Constants.PDF);
                response = client.Execute(request);
                var details = JObject.Parse(response.Content);
                requestId = ((Newtonsoft.Json.Linq.JValue)details["request_id"]).Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get the request id from api whose API status is {response.StatusCode} and request id is {requestId}: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return requestId;
        }

        public IRestResponse GetStatusresponse()
        {
            try
            {
                var client = new RestClient(Context.Properties["StatusURI"].ToString() + MetaDataValues.RequesterRefValue);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", Context.Properties["ContentType"].ToString());
                request.AddHeader("Authorization", Context.Properties["Authorization"].ToString());
                IRestResponse response = client.Execute(request);
                
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get the status response for {Context.Properties["StatusURI"].ToString() + MetaDataValues.RequesterRefValue} from api : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public IRestResponse GetStatusresponseAfterDeleting()
        {
            try
            {
                var client = new RestClient(Context.Properties["StatusURI"].ToString() + MetaDataValues.RequesterRefValue);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", Context.Properties["ContentType"].ToString());
                request.AddHeader("Authorization", Context.Properties["Authorization"].ToString());
                IRestResponse response = client.Execute(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get the status response after deleting for {Context.Properties["StatusURI"].ToString() + MetaDataValues.RequesterRefValue} from api : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public IRestResponse CreateRequestAndGetFacilityName()
        {
            IRestResponse response = null;
            try
            {
                string requestId = string.Empty;
                var client = new RestClient(Context.Properties["FacilityResponseURI"].ToString());
                //client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Basic d2MtZXh0YXBpOnRlc3RpbmcxMjMh");
                response = client.Execute(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get the request id from api whose API status is {response.StatusCode} and whose content is {response.Content} Exception message is : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public Dictionary<string, string> ParseResponseData(IRestResponse response, string facilityName)
        {
            try
            {
                Dictionary<string, string> rowData = new Dictionary<string, string>();
                JsonDocument data = JsonDocument.Parse(response.Content);
                var root = data.RootElement;

                for (int i = 0; i < root.GetArrayLength(); i++)
                {
                    if (root[i].GetProperty("facility_name").ToString().Contains(facilityName))
                    {
                        rowData.Add("facility_name", root[i].GetProperty("facility_name").ToString());
                        rowData.Add("facility_code", root[i].GetProperty("facility_code").ToString());
                        break;
                    }

                }
                return rowData;
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
