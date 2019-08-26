using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPost.Modules.Users.Shared.Commands;
using NPost.Modules.Users.Shared.DTO;
using NPost.Modules.Users.Shared.Queries;
using NPost.Shared;

namespace NPost.Modules.Users.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public UsersApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        [HttpGet("_meta")]
        public ActionResult<string> Meta() => "Users module";

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> Me()
        {
            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(SignUp command)
        {
            await _dispatcher.SendAsync(command);

            return CreatedAtAction(nameof(Me), null);
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<JwtDto>> SignIn(GetToken query)
        {
            var jwt = await _dispatcher.QueryAsync(query);

            return Ok(jwt);
        }
    }
}