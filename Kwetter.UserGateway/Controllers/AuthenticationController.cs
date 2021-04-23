using Kwetter.ServiceLayer.Manager;
using Kwetter.ServiceLayer.Model;
using Kwetter.ServiceLayer.Validation;
using Kwetter.UserGateway.VIewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kwetter.UserGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<AuthenticationController> logger;

        /// <summary>
        /// Authentication manager for auth related operations.
        /// </summary>
        private readonly AuthenticationManager manager = null;


        public AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationManager manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                AuthenticationValidation.ValidateUsername(model.Username);
                AuthenticationValidation.ValidatePassword(model.Password);
                var token = await this.manager.TrySignIn(model.Username, model.Password).ConfigureAwait(false);
                return Ok(token);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError("Exception occurred in register function", ex);
                return BadRequest("Failed to authenticate account");
            }
        }

        //POST api/<AuthenticationController>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                AuthenticationValidation.ValidateUsername(model.Username);
                AuthenticationValidation.ValidatePassword(model.Password, model.PasswordRepeated);

                var registerState = await this.manager.TrySignUp(model.Username, model.Password).ConfigureAwait(false);
                if(registerState)
                {
                    return Ok("Succesfully registered");
                }
                return Ok("Failure noob");
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occurred in login function", ex);
                return BadRequest("Failed to register account");
            }
        }
    }
}
