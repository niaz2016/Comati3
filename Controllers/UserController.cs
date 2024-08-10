using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            else {
                return AddOrUpdateUser(user);
            }
        }
        [HttpPost]
        private IActionResult AddOrUpdateUser(UserDTO user)
        {
            Person p = user.ToModel<Person>();
            if (user.Id == 0)
            {
                _comatiContext.Users.Add(p);
                _comatiContext.SaveChanges();
                p.Mgr = _comatiContext.Persons.Where(_p => _p.Phone == user.Phone).Select(p=>p.Id).FirstOrDefault();
                _comatiContext.SaveChanges();
                return Ok(p);
            }
            else
            {
                p.Mgr= user.Id;
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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return View();
        }

        
    }
}
