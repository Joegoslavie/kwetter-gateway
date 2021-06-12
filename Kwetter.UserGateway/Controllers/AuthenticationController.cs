using Kwetter.Business.Exceptions;
using Kwetter.Business.Manager;
using Kwetter.Business.Validation;
using Kwetter.UserGateway.VIewModels.Account;
using Kwetter.UserGateway.VIewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
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
        private readonly AuthenticationManager authManager;

        /// <summary>
        /// Account manager for doing account related operations.
        /// </summary>
        private readonly AccountManager accountManager;

        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            AuthenticationManager authManager, 
            AccountManager accountManager)
        {
            this.logger = logger;
            this.authManager = authManager;
            this.accountManager = accountManager;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        [Route("login2")]
        public async Task<IActionResult> Login2([FromBody] LoginViewModel model)
        {

            // little hack to continue development when I dont have the full env ready.
            var authModel = JsonConvert.DeserializeObject<AuthenticationResultModel>(System.IO.File.ReadAllText(@"C:\Users\Hugo\source\repos\C#\KwetterGateways\Kwetter.UserGateway\kwetter-data.json"));
            await Task.Delay(TimeSpan.FromSeconds(1));

            return Ok(authModel);
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

                var account = await this.authManager.SignIn(model.Username, model.Password).ConfigureAwait(false);
                account.Profile = await this.accountManager.GetProfile(account, includeTweets: true, withFollowings: true).ConfigureAwait(false);

                var accountModel = new AccountViewModel(account);
                var authModel = new AuthenticationResultModel(accountModel);

                System.IO.File.WriteAllText("kwetter-data.json", JsonConvert.SerializeObject(authModel));
                return Ok(authModel);
            }
            catch (AuthenticateException exception)
            {
                return BadRequest(new AuthenticationResultModel(exception.ErrroCode, exception.Message));
            }
            catch (ProfileException exception)
            {
                return BadRequest(new AuthenticationResultModel(false, exception.Message, null));
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthenticationResultModel(false, "Oops.. something went wrong.", null));
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

                var account = await this.authManager.Register(model.Username, model.Password, email: model.Email).ConfigureAwait(false);
                
                // Give Kafka time to process the profile.
                await Task.Delay(TimeSpan.FromSeconds(3));
                account.Profile = await this.accountManager.GetProfile(account, includeTweets: false, withFollowings: false).ConfigureAwait(false);
                return Ok(new AuthenticationResultModel(new AccountViewModel(account)));
            }
            catch (AuthenticateException exception)
            {
                return BadRequest(new AuthenticationResultModel(exception.ErrroCode, exception.Message));
            }
            catch (ProfileException exception)
            {
                return BadRequest(new AuthenticationResultModel(false, exception.Message, null));
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthenticationResultModel(false, "Oops.. something went wrong.", null));
            }
        }
    }
}
