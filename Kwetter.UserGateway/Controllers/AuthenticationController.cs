using Kwetter.ServiceLayer.Manager;
using Kwetter.ServiceLayer.Managers;
using Kwetter.ServiceLayer.Validation;
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
        private readonly ILogger<AuthenticationController> logger;

        private readonly AuthenticationManager manager = null;

        public AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationManager manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] string username, string password)
        {
            try
            {
                AuthenticationValidation.ValidateUsername(username);
                AuthenticationValidation.ValidatePassword(password);
                var token = await this.manager.TrySignIn(username, password).ConfigureAwait(false);
                return Ok(token);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError("Exception occurred in register function", ex);
                throw;
            }
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        public void Register([FromBody] string username, string password, string verifyPassword)
        {
            try
            {
                AuthenticationValidation.ValidateUsername(username);
                AuthenticationValidation.ValidatePassword(password, verifyPassword);

                this.manager.TrySignUp(username, password);
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occurred in login function", ex);
                throw;
            }
        }
    }
}
