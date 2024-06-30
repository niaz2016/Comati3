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
        public string Post([FromBody] ComatiPaymentPostDTO comatiPayment)
        {
            ComatiPayment CP= comatiPayment.ToModel<ComatiPayment>();
            _comatiContext.ComatiPayments.Add(CP);
            _comatiContext.SaveChanges();
            var paidFor = _comatiContext.ComatiPayments.Where(ComatiPayment => ComatiPayment.Id == CP.Id).Select(cp => new 
            {
                cp.Amount,
                cp.Person.Name,
            }).First();
            return $"Comati Paid Successfully for {paidFor.Name} amound received {paidFor.Amount}";
        }
        // GET: api/<ComatiPaymentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ComatiPaymentController>/5
        [HttpGet("{id}")]
        public ComatiPaymentPostDTO Get(int id)
        {
            ComatiPaymentPostDTO cp = _comatiContext.ComatiPayments.Where(comatiPayment => comatiPayment.Id == id).Select(comatiPayment => new ComatiPaymentPostDTO
            {
                Amount= comatiPayment.Amount,
                ComatiId= comatiPayment.Id,
                MemberId= comatiPayment.Id,
                PaymentDate= comatiPayment.PaymentDate,
            }).First();
            return cp;
        }

        /*// PUT api/<ComatiPaymentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<ComatiPaymentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _comatiContext.ComatiPayments.Find(id).IsDeleted = true;
        }
    }
}
