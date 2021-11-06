using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Core.Services;
using NotificationService.Core.Services.Inputs;

namespace NotificationService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserInput userInput)
        {
            try
            {
                var user = await _userService.CreateAsync(userInput);
                return Ok(user);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message); 
            }
        }

        [HttpDelete("{userAlias}")]
        public async Task<IActionResult> Delete(string userAlias)
        {
            try
            {
                var isDeleted = await _userService.DeleteAsync(userAlias);
                return Ok(new
                {
                    success = true,
                    userAlias = userAlias,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
