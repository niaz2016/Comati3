using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Comati3.DTOs;

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
        [HttpGet("defaulters")]
        public IActionResult AnyDefaulter(int comatiId)
        {
            Comati c = _comatiContext.Comaties.Find(comatiId);
            DateTime currentDate = DateTime.Now;

            // Calculate the total number of months passed
            int yearsDifference = currentDate.Year - c.Start_Date.Year;
            int monthsDifference = currentDate.Month - c.Start_Date.Month;
            int totalMonthsPassed = (yearsDifference * 12) + monthsDifference+1; //we have to count first month as well,
            //Amount that must have been paid until this month = shouldBe

            IEnumerable<AllTimeDefaulterDTO> defaultersList = [.. _comatiContext.Members.Where(c => c.ComatiId == comatiId).Select(d => new AllTimeDefaulterDTO
            {
                Name = d.Person.Name,
                MemberId = d.Id,
                TotalPaid = d.ComatiPayments.Sum(a=>a.Amount),
                AmountOverdue = d.Amount*totalMonthsPassed - d.ComatiPayments.Sum(a => a.Amount),
            }).Where(d=>d.TotalPaid<d.AmountOverdue*totalMonthsPassed)];
            return Ok(defaultersList);
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
