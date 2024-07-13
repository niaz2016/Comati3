using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Comati3.DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComatiPaymentController : ControllerBase
    {
        readonly ComatiContext _comatiContext;
        public ComatiPaymentController(ComatiContext comatiContext)
        {
            _comatiContext = comatiContext;
        }
        // POST api/<PersonController>
        [HttpPost]
        public IActionResult Post([FromBody] ComatiPaymentPostDTO comatiPayment)
        {

            ComatiPayment cm = comatiPayment.ToModel<ComatiPayment>();
            _comatiContext.ComatiPayments.Add(cm);
            _comatiContext.SaveChanges();

            return Ok(cm);
        }
        
        [HttpGet("memberPayments")]
        public IEnumerable<PaymentGetDTO> MemberPayments(int memberId)
        {
            IEnumerable<PaymentGetDTO> payments = _comatiContext.ComatiPayments.Where(cp => cp.MemberId == memberId).Select(c => new PaymentGetDTO
            {
                PaymentDate = c.PaymentDate,
                Amount = c.Amount,
                Remarks = c.Remarks,
            }).ToList();
            return payments;
        }

        // GET api/<ComatiPaymentController>/5
        [HttpGet]
        public string GetPaymentStatus(DateOnly date, int comatiId)
        {
            //PaymentGetDTO payStatus= _comatiContext.ComatiPayments.
            

                return  "Method not implemented";
        }

    }
}
