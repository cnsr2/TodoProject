using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            if (RegisterValidate(model))
            {
                User user = new User { userName = model.name, userPassword = model.pw };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return Ok(user.Id);
            }
            else
            {
                return BadRequest(new { error = "Registration Failed, Information Does Not Match" });
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<User>> LoginUser(LoginSet model)
        {
            var userCheck = _dbContext.Users.FirstOrDefault(x => x.userName == model.name);
            if (userCheck == null)
            {
                return NotFound("User Not Found");
            }
            if (model.name != null || model.pw != null)
            {
                if (_dbContext.Users != null)
                {
                    if (userCheck.userPassword == model.pw)
                    {
                        List<Todo> todoList = new List<Todo>();
                        foreach (var item in _dbContext.Todo)
                        {
                            if (userCheck.Id == item.userId)
                            {
                                todoList.Add(item);
                            }
                        }
                        UserTodo newUser = new UserTodo() { todoList = todoList , userId = userCheck.Id};
                        return Ok(newUser);
                    }
                    return BadRequest(new { error = "Wrong Password" });

                }
                return BadRequest(new { error = "Login Failed" });

            }
            return NotFound("username or password cannot be blank");
        }
        private bool RegisterValidate(RegisterSet model)
        {
            if (model.name != null || model.pw != null || model.valPw != null)
            {
                if (model.name.Length >= 3)
                {
                    if (model.pw.Length >= 8 && model.valPw.Length >= 8)
                    {
                        if (model.pw == model.valPw)
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
