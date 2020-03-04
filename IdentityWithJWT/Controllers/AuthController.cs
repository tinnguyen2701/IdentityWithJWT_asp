using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithJWT.Api.Services;
using IdentityWithJWT.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithJWT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if(result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
    }
}