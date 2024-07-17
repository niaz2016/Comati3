using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Comati3.DTOs;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;

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
            if (comatiMember.Id == null || comatiMember.Id == 0)
            {
                _comatiContext.Members.Add(cm);
            }
            else
            {
                _comatiContext.Members.Update(cm);
            }
            _comatiContext.SaveChanges();
            return Ok(cm);
        }
        // GET: api/<ComatiMemberGetDTOController>
        [HttpGet]
        public IEnumerable<ComatiMemberGetDTO> GetMembers(int comatiId)
        {
            IEnumerable<ComatiMemberGetDTO> comatiMembers = _comatiContext.Members
                .Where(member => member.ComatiId == comatiId && member.IsDeleted == false)
                .Select(member => new ComatiMemberGetDTO
                {
                    Id = member.Id,
                    PersonId = member.PersonId,   //needed to update comatiMember 
                    ComatiName = member.Comati.Name,
                    Name = member.Person.Name,
                    OpeningMonth = member.OpeningMonth,
                    Amount = member.Amount,
                    Remarks = member.Remarks,
                    
                });

            return comatiMembers;
        }
        [HttpGet("memberId")]
        public ComatiMemberGetDTO GetMember(int memberId) {

            ComatiMemberGetDTO member = _comatiContext.Members.Where(m => m.Id == memberId && m.IsDeleted==false).Select(cm => new ComatiMemberGetDTO
            {
                ComatiName = cm.Comati.Name,
                Amount = cm.Amount,
                OpeningMonth= cm.OpeningMonth,
                Remarks = cm.Remarks,
                PersonId= memberId,
            }).FirstOrDefault();
            return member;
        }
        // DELETE api/<ComatiMemberController>/5
        [HttpDelete]
            public IActionResult Delete(int id)
            {
                _comatiContext.Members.Find(id).IsDeleted = true;
            _comatiContext.SaveChanges();
            return Ok(id);
            }
    }
}

