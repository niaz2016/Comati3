using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  

namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComatiController : ControllerBase
    {
        readonly ComatiContext _comatiContext;

        public ComatiController(ComatiContext comatiContext)
        {
            _comatiContext = comatiContext;
        }
        // POST api/<ComatiController>
        [HttpPost]
        public IActionResult Post([FromBody] ComatiPostDTO comati)
        {
            Comati c = comati.ToModel<Comati>();
            if (comati.Id == null || comati.Id == 0) { _comatiContext.Comaties.Add(c); }
            else { _comatiContext.Comaties.Update(c); }
            _comatiContext.SaveChanges();
            return Ok(c);
        }
        //GET: api/<ComatiController>
        [HttpGet]
        public IEnumerable<ComatiGetDTO> ComatiesByMgrId(int MgrId)
        {                                                                   
            IEnumerable<ComatiGetDTO> comaties = _comatiContext.Comaties.Include(y => y.Payments).Include(y => y.Members).ThenInclude(p => p.Person).ToList().Where(comati => comati.ManagerId == MgrId && comati.IsDeleted == false).Select(comati => new ComatiGetDTO
            {
                Id = comati.Id,
                ManagerId = comati.ManagerId,
                Name = comati.Name,
                Start_Date = comati.Start_Date,
                End_Date = comati.Start_Date.AddMonths((comati.Members.Sum(member => member.Amount) / comati.Per_Head) - 1),
                Per_Head = comati.Per_Head,
                Remarks = comati.Remarks,
                TotalMembers = comati.Members != null ? comati.Members.Where(c => c.IsDeleted == false).Count() : 0,
                TotalComati = comati.Members.Where(m => m.IsDeleted == false).Sum(a => a.Amount),
                TotalCollected = comati.Payments?.Where(p => p.PaymentDate.Month == DateTime.Now.Month).Sum(a => a.Amount) ?? 0,
                Defaulters = comati.Members.Select(member => new DefaulterDTO
                {
                    MemberId = member.Id,
                    ComatiId = member.ComatiId,
                    Phone = member.Person.Phone,
                    Name = member.Person.Name,
                    Amount = member.Amount,
                    Address = member.Person.Address,
                    Remarks = member.Remarks,
                    IsNotPaid = (bool?)!(member.ComatiPayments?.Any() == true &&
                   member.ComatiPayments.OrderBy(y => y.PaymentDate)
                                       .LastOrDefault()?.PaymentDate.Month >= DateTime.Now.Month) ?? true
                }).Where(y => y.IsNotPaid).ToList(),
            });
            return comaties;
        }
        // Get a comati, many details
        [HttpGet("comati")]
        public ComatiGetDTO GetComati(int comatiId)
        {
            ComatiGetDTO comati = _comatiContext.Comaties.Include(y => y.Payments).Include(y => y.Members).ThenInclude(y => y.Person).ToList().Where(c => c.Id == comatiId && c.IsDeleted == false).Select(c => new ComatiGetDTO
            {
                Id = c.Id,
                ManagerId = c.ManagerId,
                Name = c.Name,
                Start_Date = c.Start_Date,
                End_Date = c.Start_Date.AddMonths(c.Members != null ? c.Members.Count : 0),
                Per_Head = c.Per_Head,
                Remarks = c.Remarks,
                TotalMembers = c.Members != null ? c.Members.Count() : 0,
                TotalComati = c.Members.Select(a => a.Amount).Sum(),
                TotalCollected = c.Payments?.Where(p => p.PaymentDate.Month == DateTime.Now.Month).Sum(a => a.Amount) ?? 0,
                Defaulters = c.Members.Select(member => new DefaulterDTO{
                    MemberId = member.Id,
                    ComatiId = member.ComatiId,
                    Phone = member.Person.Phone,
                    Name = member.Person.Name,
                    Amount = member.Amount,
                    Address = member.Person.Address,
                    Remarks = member.Remarks,
                    IsNotPaid = (bool?)!(member.ComatiPayments?.Any() == true &&
                   member.ComatiPayments.OrderBy(y => y.PaymentDate)
                                       .LastOrDefault()?.PaymentDate.Month >= DateTime.Now.Month) ?? true
        }).Where(y=>y.IsNotPaid).ToList(),

            }).First();
           // int months = comati.Members.Count(a => a.Amount).Sum()/c.Per_Head;
            
            return comati;
        }
        [HttpGet("defaulters")]
        public DefaulterDTO Defaulters(int comatiId)
        {
            DefaulterDTO defaulters = _comatiContext.Members.Select(def=>new DefaulterDTO
            {
                Name = def.Person.Name,
                Amount = def.Amount,
                IsNotPaid = (bool?)(def.ComatiPayments.Count == 0 || def.ComatiPayments.OrderBy(y => y.PaymentDate).LastOrDefault().PaymentDate.Month < DateTime.Now.Month) ?? false

            }).Where(y=>y.IsNotPaid).First();
            return defaulters;
        }


        // DELETE api/<ComatiController>/5
        [HttpDelete("delete")]
        public IActionResult Delete(int comatiId)
        {
            if (_comatiContext.Comaties == null)
            {
                return NotFound("Comaties collection is null.");
            }

            var comati = _comatiContext.Comaties.Find(comatiId);
            if (comati == null)
            {
                return NotFound($"Comati with id {comatiId} not found.");
            }

            comati.IsDeleted = true;
            _comatiContext.SaveChanges();

            return Ok(comatiId);
        }

    }
}
