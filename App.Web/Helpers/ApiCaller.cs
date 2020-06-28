using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace AppProj.Web.Helpers
{
    public static class ApiCaller
    {
        public static IEnumerable<StaffProfile> GetEmployeeByPIN(string pin)
        {
            string URL = "http://api.brac.net/v1/staffs";
            string urlParameters = "?Key=d65808a7-699f-4d5c-88ee-01951e675cf2&fields=pin,StaffName,BloodGroup,Designationname,EmailID,MobileNo,dateofbirth,sex,projectname,branchname,districtname,Grade,PermanentAddressDistrictName,JoiningDate,TransferDate,UpazilaName&q=pin=" + pin;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.

                try
                {
                    var dataString = response.Content.ReadAsAsync<string>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    return JsonConvert.DeserializeObject<IEnumerable<StaffProfile>>(dataString);
                }
                catch
                {

                }
            }

            return null;

        }

        public static IEnumerable<StaffProfile> GetEmployeeByMobile(string mobile)
        {
            string URL = "http://api.brac.net/v1/staffs";
            string urlParameters = "?Key=d65808a7-699f-4d5c-88ee-01951e675cf2&fields=pin,StaffName,Designationname,EmailID,MobileNo,dateofbirth,sex,projectname,branchname,districtname&q=mobileno=" + mobile;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                try
                {
                    var dataString = response.Content.ReadAsAsync<string>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    return JsonConvert.DeserializeObject<IEnumerable<StaffProfile>>(dataString);
                }
                catch
                {

                }
            }

            return null;

        }
    }
}