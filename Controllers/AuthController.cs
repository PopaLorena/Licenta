using Licenta.Context;
using Licenta.Dto;
using Licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User user = new User();
        private readonly ContextDb _context;
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration, ContextDb context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok( _context.Users.ToList());

        }

        [HttpGet]
        [Route("memberIdNotAdminYet/{id}")]
        public async Task<IActionResult> memberIdNotAdminYet(int id)
        {
            var existingUser = _context.Users.SingleOrDefault(x => x.Id == id);
            if(existingUser.Role == "Admin")
                return Ok(false);
            return Ok(true);
        }

        [HttpPatch, Authorize(Roles = "User,Admin")]
        [Route("edit/{newPassword}")]
        public async Task<IActionResult> editPassword(UserDto request, string newPassword)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(x => x.Username == request.Username);
                if (existingUser != null)
                {
                    if (!VerifyPasswordHask(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt))
                    {
                        return BadRequest("Wrong password");
                    }
                    CreatePassworHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                    existingUser.PasswordHash = passwordHash;
                    existingUser.PasswordSalt = passwordSalt;
                    _context.Users.Update(existingUser);
                    _context.SaveChanges();

                    return Ok(existingUser);
                }
                return BadRequest("User not found");
            }
            catch (Exception e)
            {
                return BadRequest("User not found");
            }
        }

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("setAsAdmin/{id}")]
        public async Task<IActionResult> setAsAdmin(int id)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(x => x.Id == id);
                if (existingUser != null)
                {
                    existingUser.Role = "Admin";
                    _context.Users.Update(existingUser);
                    _context.SaveChanges();

                    return Ok(existingUser);
                }
                return BadRequest("User not found");
            }
            catch (Exception e)
            {
                return BadRequest("User not found");
            }
        }

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("setAsUser/{id}")]
        public async Task<IActionResult> setAsUser(int id)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(x => x.Id == id);
                if (existingUser != null)
                {
                    existingUser.Role = "User";
                    _context.Users.Update(existingUser);
                    _context.SaveChanges();

                    return Ok(existingUser);
                }
                return BadRequest("User not found");
            }
            catch (Exception e)
            {
                return BadRequest("User not found");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {

            var existingUser = _context.Users.SingleOrDefault(x => x.Username == request.Username);
            if(existingUser!= null)
                return BadRequest("Username is already taken");

            var user = new User();
            CreatePassworHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = request.Username;
            user.Role = request.Role;

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpGet]
        [Route("getRole/{Username}")]
        public async Task<IActionResult> GetMembers(string Username)
        {
            var user =  _context.Users.SingleOrDefault(x => x.Username == Username);
            return Ok(user.Role);
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == Id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }



        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = new User();
            try
            {
                user = _context.Users.SingleOrDefault(x => x.Username == request.Username);
            }
            catch (Exception e)
            {
                return BadRequest("User not found");
            }

            if(user == null)
            {
                return BadRequest("User not found");

            }

            if (!VerifyPasswordHask(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
               );

            var jtw = new JwtSecurityTokenHandler().WriteToken(token);
            return jtw;
        }
        private void CreatePassworHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHask(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
