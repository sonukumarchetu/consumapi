using ConsumeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsumeApi.Models;
using System.Text;

namespace ConsumeApi.Controllers
{
    [Route("Employee")]
    
    public class EmployeeController : Controller
    {
        List<Employee> Api = new List<Employee>();
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = new HttpClient();
                string apiURL = "https://localhost:44305/Employees/getlist";
               // UriBuilder builder = new UriBuilder();
                HttpResponseMessage response = client.GetAsync(apiURL).Result;
                var responsebody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<List<Employee>>(responsebody);
                    Api = data;
                }

                return View(Api);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("Createdata")]
        public async Task <IActionResult> Create(Employee employee)
        {     
            {
                Employee employeedata = new Employee();
                HttpClient httpClient = new HttpClient();
                string serializedCurrency = JsonConvert.SerializeObject(employee);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = new HttpMethod("POST");
                httpRequestMessage.RequestUri = new Uri ("https://localhost:44305/Employees/CreateEmployee");
                httpRequestMessage.Content = new StringContent(serializedCurrency);
                httpRequestMessage.Content.Headers.ContentType= new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);
                {
                    return RedirectToAction("Index");

                }
            } 
        }
        [Route("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44305/Employees/DeleteEmployee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
                return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Update")]
        public async Task< IActionResult> Update(int id)
        {
            Employee employeedata = new Employee();
            var client = new HttpClient();
            string apiURL = "https://localhost:44305/Employees/UpdateEmployee/" + id;
            UriBuilder builder = new UriBuilder();
            HttpResponseMessage response = client.GetAsync(apiURL).Result;
            var responsebody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<Employee>(responsebody);
                employeedata = data;
            }
            return View(employeedata);
        }



        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(Employee employee)
        {
            Employee employeedata = new Employee();
            HttpClient httpClient = new HttpClient();
            string serializedCurrency = JsonConvert.SerializeObject(employee);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = new HttpMethod("PUT");
            httpRequestMessage.RequestUri = new Uri("https://localhost:44305/Employees/UpdateEmployee");
            httpRequestMessage.Content = new StringContent(serializedCurrency);
            httpRequestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);
            return RedirectToAction("Index");
        }
    }
}

    

