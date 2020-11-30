using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetIdentityDemo.Api.Services;
using AspNetIdentityDemo.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserServices _userService;
        private IMailService _mailService;

        public AuthController(IUserServices userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        [HttpPost("Register")]
        //[Route("Register")]
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

            return BadRequest("Some Properties are not valid");
        }



        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendMailAsync(model.Email, "Log in alert", "<h1>Hey!, new LOg in to your account noticed<h1> <p>New log in to your account at " + DateTime.Now + "</p>");
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some Properties are not valid");
        }
    }
}
