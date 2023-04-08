using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    _logger.LogError(string.Join(",", errors));
                    return BadRequest(errors);
                }

                var user = Models.User.Map(userRequest);

                if (_userService.IsUserDuplicated(user) == true)
                {
                    _logger.LogError("The user is duplicated");
                    return Conflict("The user is duplicated");
                }

                await _userService.SaveUserToFile(user);

                _logger.LogInformation("User Created");
                return Ok("User Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
