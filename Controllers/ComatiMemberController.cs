using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Comati3.DTOs;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComatiMemberController : ControllerBase
    {
        private readonly ComatiContext _comatiContext;
        public ComatiMemberController(ComatiContext comatiContext)
        {
            _comatiContext = comatiContext;
        }
        // POST api/<PersonController>
        [HttpPost]
        public IActionResult Post([FromBody] ComatiMemberPostDTO comatiMember)
        {

            ComatiMember cm = comatiMember.ToModel<ComatiMember>();
            _comatiContext.Members.Add(cm);
            _comatiContext.SaveChanges();

            return Ok(cm);
        }
        // GET: api/<ComatiMemberGetDTOController>
        [HttpGet]
        public IEnumerable<ComatiMemberGetDTO> GetMember(int comatiId)
        {
            IEnumerable<ComatiMemberGetDTO> comatiMembers = _comatiContext.Members
                .Where(member => member.ComatiId == comatiId)
                .Select(member => new ComatiMemberGetDTO
                {
                    ComatiId = member.ComatiId,
                    Name= member.Name,
                    OpeningMonth = member.OpeningMonth,
                    Amount = member.Amount,
                    Remarks = member.Remarks,
                })
                .ToList();

            return comatiMembers;
        }
        [HttpGet("mmid")]
        public async Task<ActionResult<int>> GetMaxMemberShipId(int comatiId)
        {
            var maxMemberShipId = await _comatiContext.Members
                .Where(m => m.ComatiId == comatiId)
                .Select(m => m.Id).MaxAsync();

            return Ok(maxMemberShipId);
            // DELETE api/<ComatiMemberController>/5
            [HttpDelete("{id}")]
            void Delete(int id)
            {
                _comatiContext.Members.Find(id).IsDeleted = true;
            }
        }
    }
}
