using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            PageWrapper pageWrapper = new PageWrapper();
            List<People> reservationList = new List<People>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:51322/api/People"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pageWrapper = JsonConvert.DeserializeObject<PageWrapper>(apiResponse);
                }
            }
            return View(pageWrapper.data);
        }

        public ViewResult GetPerson() => View();

        [HttpPost]
        public async Task<IActionResult> GetPerson(int id)
        {
            People reservation = new People();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:51322/api/People/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ;
                        reservation = JsonConvert.DeserializeObject<People>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(reservation);
        }

        public ViewResult AddPeople() => View();

        [HttpPost]
        public async Task<IActionResult> AddPeople(People reservation)
        {
            People receivedReservation = new People();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:51322/api/People", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedReservation = JsonConvert.DeserializeObject<People>(apiResponse);
                }
            }
            return View(receivedReservation);
        }

      

        public async Task<IActionResult> UpdatePeople(int id)
        {
            People reservation = new People();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:51322/api/People/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<People>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePeople(People reservation)
        {
            People receivedReservation = new People();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(reservation.PeopleId.ToString()), "Id");
                content.Add(new StringContent(reservation.FirstName), "Name");
                content.Add(new StringContent(reservation.LastName), "StartLocation");
                content.Add(new StringContent(reservation.PhoneNumber), "EndLocation");

                using (var response = await httpClient.PutAsync("https://localhost:51322/api/People", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedReservation = JsonConvert.DeserializeObject<People>(apiResponse);
                }
            }
            return View(receivedReservation);
        }

        public async Task<IActionResult> UpdatePeoplePatch(int id)
        {
            People reservation = new People();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:51322/api/People/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<People>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePeoplePatch(int id, People reservation)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:51322/api/People/" + id),
                    Method = new HttpMethod("Patch"),

                    Content = new StringContent(
                        "[{ \"op\":\"replace\", \"path\":\"firstName\", \"value\":\""
                        + reservation.FirstName
                        + "\"},{ \"op\":\"replace\", \"path\":\"lastName\", \"value\":\""
                        + reservation.LastName + "\"}]"
                    , Encoding.UTF8
                    , "application/json-patch+json")
                };

                var response = await httpClient.SendAsync(request);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int ReservationId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:51322/api/People/" + ReservationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
        
    }
}