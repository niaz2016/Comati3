using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

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
        {                                                                   //.Include(y=>y.Payments).Include(y=>y.Members).ThenInclude(y=>y.Person).ToList()
            IEnumerable<ComatiGetDTO> comaties = _comatiContext.Comaties.Where(comati => comati.ManagerId == MgrId && comati.IsDeleted == false).Select(comati => new ComatiGetDTO
            {
                Id = comati.Id,
                ManagerId = comati.ManagerId,
                Name = comati.Name,
                Start_Date = comati.Start_Date,
                End_Date =  comati.Start_Date.AddMonths((comati.Members.Sum(member=>member.Amount)/comati.Per_Head)-1),
                Per_Head = comati.Per_Head,
                Remarks = comati.Remarks,
                TotalMembers = comati.Members != null ? comati.Members.Where(c=>c.IsDeleted==false).Count() : 0,
                TotalComati = comati.Members.Where(m=>m.IsDeleted==false).Sum(a => a.Amount),
                
                TotalCollected = comati.Payments.Sum(a => a.Amount),
                Defaulters = comati.Members.Where(member=>member.IsDeleted==false).Select(member => new DefaulterDTO
                {
                   MemberId = member.Id,
                   Name= member.Person.Name,
                   Phone = member.Person.Phone,
                   Amount =  member.Amount,
                   IsNotPaid = (bool?)(member.ComatiPayments.Count == 0 || member.ComatiPayments.OrderBy(y => y.PaymentDate).LastOrDefault().PaymentDate.Month < DateTime.Now.Month) ?? false,
                   Address = member.Person.Address,
                   Remarks = member.Person.Remarks,
                }).Where(y => y.IsNotPaid).ToList(),
            }
            );
            return comaties;
        }
        // Get a comati, many details
        [HttpGet("comati")]
        public ComatiGetDTO GetComati(int comatiId)
        {

            ComatiGetDTO comati = _comatiContext.Comaties.Where(c => c.Id == comatiId && c.IsDeleted == false).Select(c => new ComatiGetDTO
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
                TotalCollected = c.Payments.Select(c => c.Amount).Sum(),
                Defaulters = c.Members.Select(member => new DefaulterDTO{
                    Name = member.Person.Name,
                    Amount = member.Amount,
                    IsNotPaid = (bool?)( member.ComatiPayments.Count ==0 || member.ComatiPayments.OrderBy(y=>y.PaymentDate).LastOrDefault().PaymentDate.Month < DateTime.Now.Month) ?? false
                }).Where(y=>y.IsNotPaid).ToList(),

            }).First();
           // int months = comati.Members.Count(a => a.Amount).Sum()/c.Per_Head;
            
            return comati;
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

            return Ok($"Delete Success");
        }

    }
}
