using Kwetter.ServiceLayer.Manager;
using Kwetter.ServiceLayer.Model;
using Kwetter.ServiceLayer.Validation;
using Kwetter.UserGateway.Factory;
using Kwetter.UserGateway.VIewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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

                var account = await this.manager.SignIn(model.Username, model.Password).ConfigureAwait(false);
                var viewModel = ModelFactory.Convert(account);

                return Ok(viewModel);
            }
            catch (AuthenticationException exception)
            {
                this.logger.LogError($"Login exception occured for user {model.Username}", exception);
                return BadRequest(exception.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occurred in login operation", ex);
                return BadRequest();
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

                var account = await this.manager.Register(model.Username, model.Password).ConfigureAwait(false);
                var viewModel = ModelFactory.Convert(account);

                return Ok(viewModel);
            }
            catch (AuthenticationException exception)
            {
                this.logger.LogError($"Registration exception occured ", exception, model.Username);
                return BadRequest(exception.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occurred in login", ex);
                return BadRequest();
            }
        }
    }
}
