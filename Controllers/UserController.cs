using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx;
using System.ComponentModel.DataAnnotations;

namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ComatiContext comatiContext) : Controller
    {
        readonly ComatiContext _comatiContext = comatiContext;

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if(user.Password.Length < 4) { return BadRequest(new { message = "Invalid Password" }); }
            else if(_comatiContext.Persons.Where(p=>p.Phone==user.Phone).Any()) { return BadRequest(new { message = "Phone already registered" }); }
            else { return AddOrUpdateUser(user); }
            
        }
        [HttpPost]
        private IActionResult AddOrUpdateUser(UserDTO user)
        {
            Person p = user.ToModel<Person>();
            if (user.Id == 0)
            {
                _comatiContext.Users.Add(p);
                _comatiContext.SaveChanges();
                return Ok(p);
            }
            else
            {
                _comatiContext.Users.Update(p);
                _comatiContext.SaveChanges();
                return Ok(p);
            }
        }
        //Get: login to a user
        [HttpGet]
        public async Task<IActionResult> LoginUser([FromQuery] LoginDTO user)

        {
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 4)
            {
                return BadRequest("Invalid Password");
            }
            if (_comatiContext.Users.Where(u=>u.Phone==user.Phone).FirstOrDefault() == null)
            { 
                return BadRequest(new { message = "No such Phone is registered" });
            }
            else
            {
                var existingUser = await _comatiContext.Users
                    .Where(u => u.Phone == user.Phone && u.Password == user.Password)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return Ok(existingUser);
                }
            }

            return BadRequest(new { message = "Wrong Password" });
        }


        // GET: api/<UserController>
        /*[HttpGet]
        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<UserDTO> p = _comatiContext.Users.Where(p => p.IsDeleted == false).Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address,
                Remarks = user.Remarks,

            });
            return p;
        }*/
        // GET: UserController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return View();
        }

        
    }
}
