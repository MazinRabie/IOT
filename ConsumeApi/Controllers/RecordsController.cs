using ConsumeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeApi.Controllers
{
    public class RecordsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7129/api/gate");
        HttpClient client;

        public RecordsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<RecordDto> records = new List<RecordDto>();
            HttpResponseMessage response = client.GetAsync(baseAddress + "/getrecords").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                records = JsonConvert.DeserializeObject<List<RecordDto>>(data);
            }

            return View(records);
        }

        [HttpGet]
        public IActionResult getClientRecords(string rfid)
        {
            List<RecordDto> records = new List<RecordDto>();
            HttpResponseMessage response = client.GetAsync(baseAddress + $"/GetClientRecord/{rfid}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                records = JsonConvert.DeserializeObject<List<RecordDto>>(data);
            }

            return View(viewName: "clientRecords", model: records);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteCRecord(string id, string rfid)
        {
            HttpResponseMessage response = await client.DeleteAsync(baseAddress + $"/DeleteClientrecord/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Record deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Error deleting record.";
            }

            return RedirectToAction("getClientRecords", new { rfid = rfid });
        }

    }
}
