using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.Entities;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace HackAtHome.SAL
{
    public static class HackAtHomeService
    {
        const string WepAPIAddress = "https://ticapacitacion.com/hackathome/";
        public static async Task<ResultInfo> AutenticateAsync(
            string studentEmail, string studentPassword)
        {
            ResultInfo Result = null;

            string EventID = "xamarin30";
            string RequestUri = "api/evidence/Authenticate";

            var User = new UserInfo
            {
                Email = studentEmail,
                Password = studentPassword,
                EventID = EventID
            };

            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(WepAPIAddress);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var JSONUserInfo = JsonConvert.SerializeObject(User);

                    HttpResponseMessage Response =
                        await Client.PostAsync(RequestUri,
                        new StringContent(JSONUserInfo.ToString(),
                        Encoding.UTF8, "application/json"));

                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();

                    Result = JsonConvert.DeserializeObject<ResultInfo>(ResultWebAPI);
                }
                catch (Exception ex)
                {

                }
                return Result;
            }
        }

        public static async Task<List<Evidence>> GetEvidencesASync(string token)
        {
            List<Evidence> Evidences = null;

            string WepAPIAddress = "https://ticapacitacion.com/hackathome/";
            string URI = $"{WepAPIAddress}api/evidence/getEvidences?token={token}";

            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(WepAPIAddress);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var Response = await Client.GetAsync(URI);

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResultWebAPI =
                            await Response.Content.ReadAsStringAsync();
                        Evidences = JsonConvert.DeserializeObject<List<Evidence>>(ResultWebAPI);

                    }
                }
                catch (Exception ex)
                {

                }
                return Evidences;
            }
        }

        public static async Task<EvidenceDetail> GetEvidenceByIDAsync(string token, int evidenceID)
        {
            EvidenceDetail Result = null;

            string URI = $"{WepAPIAddress}api/evidence/getevidencebyid?token={token}&&evidenceid={evidenceID}";

            using (var Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var Response =
                            await Client.GetAsync(URI);

                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        Result = JsonConvert.DeserializeObject<EvidenceDetail>(ResultWebAPI);
                    }
                }
                catch (System.Exception)
                {
                    
                }
            }
            return Result;
        }
    }
}