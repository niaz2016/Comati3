using Comati3.DTOs;
using Comati3.Models;
using Comati3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
namespace Comati3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ComatiContext comatiContext, Cookies cookies, PasswordHasher<object> passwordHasher) : Controller
    {
        readonly ComatiContext _comatiContext = comatiContext;
        readonly Cookies _cookies = cookies;
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
                var hashedPassword = _comatiContext.Users.Where(p=>p.Phone == user.Phone).Select(p=>p.Password).FirstOrDefault();
                var result = _passwordHasher.VerifyHashedPassword(user.Phone, hashedPassword, user.Password);
                user.Password = hashedPassword;
                if (result == PasswordVerificationResult.Success)
                {
                    Person person = _comatiContext.Persons.Where(p=> p.Phone == user.Phone).FirstOrDefault();
                    var cookieOptions = new CookieOptions
                    {
                        Path = "/", // Specify the path where the cookie is valid
                        HttpOnly = true, // The cookie is not accessible via JavaScript
                        Secure = false, // The cookie is only sent over HTTPS
                        Expires = DateTimeOffset.UtcNow.AddDays(1) // Set expiration to 1 day
                    };
                    Response.Cookies.Append(person.Phone, hashedPassword, cookieOptions);
                    return Ok(person);
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
