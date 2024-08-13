using Comati3.DTOs;
using Comati3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController( ComatiContext comatiContext, PasswordHasher<object> passwordHasher) : Controller
    {
        readonly ComatiContext _comatiContext = comatiContext;
        readonly PasswordHasher<object> _passwordHasher = passwordHasher;
        
        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if(user.Password.Length < 4) { return BadRequest(new { message = "Invalid Password" }); }
            else if(_comatiContext.Persons.Where(p=>p.Phone==user.Phone).Any()) { return BadRequest(new { message = "Phone already registered" }); }
            else {
                user.Password = _passwordHasher.HashPassword(user.Phone, user.Password);
                return AddOrUpdateUser(user);
            }
        }
        private IActionResult AddOrUpdateUser(UserDTO user)
        {
            Person p = user.ToModel<Person>();
            if (user.Id == 0)
            {
                _comatiContext.Users.Add(p);
                _comatiContext.SaveChanges();
                p.Mgr = _comatiContext.Persons.Where(_p => _p.Phone == user.Phone).Select(p=>p.Id).FirstOrDefault();
                _comatiContext.SaveChanges();
            }
            else
            {
                p.Mgr= user.Id;
                _comatiContext.Users.Update(p);
                _comatiContext.SaveChanges();
                
            }
            return Ok(p);
        }
        [HttpGet]
        public async Task<IActionResult> LoginUser([FromQuery] LoginDTO user)
        {
            string tempPass = "AQAAAAIAAYagAAAAEIR+xxlxfzjQAIsWZa/3hKtzf42cFr7K4ajJ5WujswZD/I6ffSE+pB1tN5IUcuISfw==";
            var userRecord =  _comatiContext.Users.Where(p => p.Phone == user.Phone).Select(n => new UserDTO
            {
                Id = n.Id,
                Name = n.Name,
                Phone = n.Phone,
                Password = n.Password!,
                Address = n.Address,
                Mgr = n.Mgr,
            });
            if (userRecord == null)
            {
                return BadRequest(new { message = "No such phone is registered" });
            }
            var result = _passwordHasher.VerifyHashedPassword(user.Phone, tempPass, user.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return BadRequest(new { message = "Wrong Password" });
            }
            else if (result == PasswordVerificationResult.Success)
            {
                var cookieOptions = new CookieOptions
                {
                    IsEssential = true,
                    Path = "/",
                    HttpOnly = true,  // Prevents JavaScript from accessing the cookie (security)
                    Secure = true,    // Use only HTTPS
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                var userJson = JsonSerializer.Serialize(userRecord);
                Response.Cookies.Append("User", userJson, cookieOptions);
            }
            return Ok(userRecord); // userRecord is Person stored in database
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
