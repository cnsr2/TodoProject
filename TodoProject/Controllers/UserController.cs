using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TodoProject.Dtos;
using TodoProject.Models;

namespace TodoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _dbContext;

        public UserController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToListAsync();
        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterUser(RegisterSet model)
            {
            var tempUser = _dbContext.Users.FirstOrDefault(x => x.userName == model.name);
            if (tempUser != null)
            {
                return BadRequest("such a user already exists");
            }
            if (RegisterValidate(model.name, model.pw, model.valPw))
            {
                User user = new User { userName = model.name, userPassword = model.pw };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest(new { error = "Registration Failed, Information Does Not Match" });
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<User>> LoginUser(string name, string pw)
        {
            var userCheck = _dbContext.Users.FirstOrDefault(x => x.userName  == name);
            if (userCheck == null)
            {
                return NotFound("User Not Found");
            }
            if (name != null || pw != null)
            {
                if (_dbContext.Users != null)
                {
                        if (userCheck.userPassword == pw)
                        {
                            return Ok();
                    }
                        return NotFound("Wrong Password");

                }
                return BadRequest(new { error = "Login Failed" });

            }
            return NotFound("username or password cannot be blank");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        private bool RegisterValidate(string name, string pw, string valPw)
        {
            if (name != null || pw != null || valPw != null)
            {
                if (name.Length >= 3)
                {
                    if (pw.Length >= 8 && valPw.Length >= 8)
                    {
                        if (pw == valPw)
                        {
                            return true;
                        }
                        else
                        {
                            throw new ArgumentException("Passwords do not match");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Password Must Be At Least 8 Characters.");
                    }
                }
                else
                {
                    throw new ArgumentException("Name Must Be At Least 3 Characters.");
                }
            }
            else
            {
                throw new ArgumentException("Name And Password Fields Cannot Be Left Blank.");
            }
        }

    }
}
