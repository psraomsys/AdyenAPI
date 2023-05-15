using AdyenAPI.Data;
using AdyenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Channels;
using System.Net.Http;
using Newtonsoft.Json;

namespace AdyenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly AdyenAPIContext  dbContext;
        public PaymentController(AdyenAPIContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> AddPayment([FromRoute] int id)
        {
            var payments = await dbContext.Payment.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            return Ok(payments);
        }
        [HttpPost]
        public IActionResult Index()
        {
            //Setting TLS 1.2 protocol
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //Fetch the JSON string from URL.
            List<payments> customers = new List<payments>();
            string apiUrl = "https://checkout-test.adyen.com/v69/paymentMethods";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                customers = JsonConvert.DeserializeObject<List<payments>>(response.Content.ReadAsStringAsync().Result);
            }

            //Return the Deserialized JSON object.
            return Json(customers);
        }
    }
}