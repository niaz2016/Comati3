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
        public ComatiPayment Post([FromBody] ComatiPaymentPostDTO comatiPayment)
        {
            ComatiPayment CP= comatiPayment.ToModel<ComatiPayment>();
            _comatiContext.ComatiPayments.Add(CP);
            _comatiContext.SaveChanges();
            
            return CP;
        }
        

        // GET api/<ComatiPaymentController>/5
        [HttpGet]
        public ComatiPaymentGetDTO Get(int comatiId, int memberId)
        {
            ComatiPaymentGetDTO cp = _comatiContext.ComatiPayments
                .Where(comatiPayment => comatiPayment.ComatiId == comatiId && comatiPayment.MemberId == memberId)
                .Select(comatiPayment => new ComatiPaymentGetDTO
        {
                Amount= comatiPayment.Amount,
                ComatiId = comatiPayment.ComatiId,
                MemberId = comatiPayment.MemberId,
                PaymentDate= comatiPayment.PaymentDate,
                Remarks= comatiPayment.Remarks,
            }).First();
            return cp;
        }

    }
}
