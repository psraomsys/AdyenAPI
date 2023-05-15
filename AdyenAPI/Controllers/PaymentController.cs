using AdyenAPI.Data;
using AdyenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace AdyenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly AdyenAPIContext dbContext;
        public PaymentController(AdyenAPIContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
           return Ok(await dbContext.Payment.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> AddPayment([FromRoute] Guid id)
        {
            var payments = await dbContext.Payment.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> Addpayments(AddPayments addPayments)
        {
            var payments = new payments()
            {
                Id = Guid.NewGuid(),
                merchantAccount = addPayments.merchantAccount,
                amount = addPayments.amount,
                channel = addPayments.channel,
                countryCode = addPayments.countryCode,
                shopperLocale = addPayments.shopperLocale
            };
            await dbContext.Payment.AddAsync(payments);
            await dbContext.SaveChangesAsync();

            return Ok(payments);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePayments([FromRoute] Guid id, UpdatePayments updatePayments)
        {
            var payments = await dbContext.Payment.FindAsync(id);

            if(payments != null)
            {
                payments.merchantAccount = updatePayments.merchantAccount;
                payments.amount = updatePayments.amount;
                payments.channel = updatePayments.channel;
                payments.countryCode = updatePayments.countryCode;
                payments.shopperLocale = updatePayments.shopperLocale;

                await dbContext.SaveChangesAsync();
                return Ok(payments);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePayments([FromRoute] Guid id)
        {
            var payments = await dbContext.Payment.FindAsync(id);

            if (payments != null)
            {
                dbContext.Remove(payments);
                await dbContext.SaveChangesAsync();
                return Ok(payments);
            }
            return NotFound();
        }
    }
}