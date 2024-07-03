using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            _comatiContext.Comaties.Add(c);
            _comatiContext.SaveChanges();

            return Ok(c);
        }
        //GET: api/<ComatiController>
        [HttpGet]
        public IEnumerable<ComatiGetDTO> ComatiesByMgrId(int MgrId)
        {
            IEnumerable<ComatiGetDTO> comaties = _comatiContext.Comaties.Where(i => i.ManagerId == MgrId).Select(i => new ComatiGetDTO
            {
                Id = i.Id,
                Name = i.Name,
                Start_Date = i.Start_Date,
                End_Date = i.End_Date,
                Per_Head = i.Per_Head,
                Remarks = i.Remarks,
                ManagerId = i.ManagerId,
                TotalMembers = i.Members!=null? i.Members.Count: 0,
                TotalComati = _comatiContext.Comaties
                            .Where(c => c.ManagerId == MgrId)
                            .Sum(c => c.Per_Head)
            }
            );
            return comaties;
        }

        // DELETE api/<ComatiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_comatiContext.Comaties == null)
            {
                return NotFound("Comaties collection is null.");
            }

            var comati = _comatiContext.Comaties.Find(id);
            if (comati == null)
            {
                return NotFound($"Comati with id {id} not found.");
            }

            comati.IsDeleted = true;
            _comatiContext.SaveChanges();

            return Ok();
        }

    }
}
