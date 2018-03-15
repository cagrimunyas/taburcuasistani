using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zirve.Models;
using System.Globalization;

namespace Zirve.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id,age,gender,race,admisson_source_id,admission_type_id,number_emergency,number_inpatient,number_outpatient,medical_specialty,num_medications,num_procedures,number_diagnoses,diag_1,time_in_hospital,discharge_disposition_id,readmitted_dttm,Scored_Labels,Scored_Probabilities")] hasta hasta)
        {
            if (ModelState.IsValid)
            {
                var client = new RestSharp.RestClient("https://ussouthcentral.services.azureml.net/workspaces/2dba6b937335466e994c804130fd4f0c/services/ce01cbc6d7714926ba5a160e77834589/execute?api-version=2.0&details=true");
                var request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddHeader("Postman-Token", "4576c4e3-4de5-4867-b5ad-e059f8bd9bfb");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Authorization", "Bearer LmRsMJeg49InQQXNVTsrRBF/PjDGdor4+wXbNu8NcxRTRedirlUJKrmvvTcZS0U835b7jz/JD7T9KtiRyHz6iA==");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\r\n  \"Inputs\": {\r\n    \"input1\": {\r\n      \"ColumnNames\": [\r\n        \"race\",\r\n        \"gender\",\r\n        \"age\",\r\n        \"admission_type_id\",\r\n        \"discharge_disposition_id\",\r\n        \"admission_source_id\",\r\n        \"time_in_hospital\",\r\n        \"num_lab_procedures\",\r\n        \"num_procedures\",\r\n        \"num_medications\",\r\n        \"number_outpatient\",\r\n        \"number_emergency\",\r\n        \"number_inpatient\",\r\n        \"diag_1\",\r\n        \"number_diagnoses\",\r\n        \"readmitted\"\r\n      ],\r\n      \"Values\": [\r\n        [\r\n          \"" + hasta.race + "\",\r\n          \"" + hasta.gender + "\",\r\n          \"" + hasta.age + "\",\r\n          \"" + hasta.admission_type_id + "\",\r\n          \"" + hasta.admission_type_id + "\",\r\n          \"" + hasta.admisson_source_id + "\",\r\n          \"" + hasta.time_in_hospital + "\",\r\n          \"" + hasta.num_lab_procedures + "\",\r\n          \"" + hasta.num_procedures + "\",\r\n          \"" + hasta.num_medications + "\",\r\n          \"" + hasta.number_outpatient + "\",\r\n          \"" + hasta.number_emergency + "\",\r\n          \"" + hasta.number_inpatient + "\",\r\n          \"" + hasta.diag_1 + "\",\r\n          \"" + hasta.number_diagnoses + "\",\r\n          \"NO\"\r\n        ],\r\n        [\r\n          \"" + hasta.race + "\",\r\n          \"" + hasta.gender + "\",\r\n          \"" + hasta.age + "\",\r\n          \"" + hasta.admission_type_id + "\",\r\n          \"" + hasta.admission_type_id + "\",\r\n          \"" + hasta.admisson_source_id + "\",\r\n          \"" + hasta.time_in_hospital + "\",\r\n          \"" + hasta.num_lab_procedures + "\",\r\n          \"" + hasta.num_procedures + "\",\r\n          \"" + hasta.num_medications + "\",\r\n          \"" + hasta.number_outpatient + "\",\r\n          \"" + hasta.number_emergency + "\",\r\n          \"" + hasta.number_inpatient + "\",\r\n          \"" + hasta.diag_1 + "\",\r\n          \"" + hasta.number_diagnoses + "\",\r\n          \"NO\"\r\n        ]\r\n      ]\r\n    }\r\n  },\r\n  \"GlobalParameters\":{}\r\n}", RestSharp.ParameterType.RequestBody);
                RestSharp.IRestResponse response = client.Execute(request);
                string obj = response.Content.ToString();
                string[] sub = obj.Split(',');
                hasta.Scored_Labels = sub[sub.Length - 2].Trim('"');
                string[] sub2 = sub[sub.Length - 1].Split(']');
                float prob = float.Parse(sub2[0].Trim('"'));
                if (hasta.Scored_Labels is "NO")
                {
                    int ret = (int)(prob * 100);
                    return RedirectToAction("Positive", new { p = ret });
                }
                else
                {
                    float probab = ((float)0.50 - prob) + (float)0.50;
                    int ret = (int)(probab * 100);
                    return RedirectToAction("Negative", new { p = ret});
                }

            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Positive(int p)
        {
            ViewBag.Message = "Hastayı taburcu edebilirsiniz.";
            ViewBag.Data = p;
            return View();
        }
        public ActionResult Negative(int p)
        {
            ViewBag.Message = "Hastayı taburcu ederseniz " + p.ToString() +"% ihtimalle geri dönecek!";
            ViewBag.Data = p;
            return View();
        }
    }
}