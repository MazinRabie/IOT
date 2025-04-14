using IOT.Models;
using IOT.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace ConsumeApi.Controllers
{
    public class ClientsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7129/api/gate");
        HttpClient client;
        public ClientsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult getClients()
        {
            List<Client> clients = new List<Client>();
            HttpResponseMessage response = client.GetAsync(baseAddress + $"/getclients").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                clients = JsonConvert.DeserializeObject<List<Client>>(data);

            }
            return View(viewName: "clients", model: clients);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteClient(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(baseAddress + $"/DeleteClient/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Record deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Error deleting record.";
            }

            return RedirectToAction("getClients");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            HttpResponseMessage response = await client.GetAsync(baseAddress + $"/getclient/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var clientDto = JsonConvert.DeserializeObject<UpdateClientDTO>(data);
                return View(viewName: "updateClient", clientDto);
            }

            TempData["Error"] = "Error retrieving client for editing.";
            return RedirectToAction("getclients");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UpdateClientDTO updatedClient)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedClient), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(baseAddress + $"/updateclient /{id}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Client updated successfully!";
                return RedirectToAction("getclients");
            }

            TempData["Error"] = "Error updating client.";
            return View(viewName: "updateClient", updatedClient); // Return the view with the form for user to correct any errors
        }


        [HttpGet]
        public IActionResult CreateClient()
        {
            return View(viewName: "CreateClient");
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDTO newClient)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newClient), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(baseAddress + $"/createclient", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Client created successfully!";
                return RedirectToAction("getClients");
            }

            TempData["Error"] = "Failed to create client.";
            return View("CreateClient", newClient);

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
