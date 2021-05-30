using Kwetter.Business.Manager;
using Kwetter.UserGateway.Attribute;
using Kwetter.UserGateway.VIewModels;
using Kwetter.UserGateway.VIewModels.Account;
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
    [KwetterAuthorization]
    public class ProfileController : KwetterController
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<ProfileController> logger;

        private readonly ProfileManager profileManager;
        private readonly FollowManager followManager;

        public ProfileController(ILogger<ProfileController> logger, ProfileManager profileManager, FollowManager followManager)
        {
            this.logger = logger;
            this.profileManager = profileManager;
            this.followManager = followManager;
        }

        [HttpGet]
        public async Task<IActionResult> View(string username)
        {
            try
            {
                var profile = await this.profileManager.Get(username).ConfigureAwait(false);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("follow")]
        public async Task<IActionResult> Follow(int userId)
        {
            try
            {
                var currentUser = base.GetAuthenticatedUser();
                var result = await this.followManager.ToggleFollow(currentUser.Id, userId).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProfileViewModel model)
        {
            try
            {
                var currentUser = base.GetAuthenticatedUser();
                var updatedProfile = await this.profileManager.Update(currentUser.Id, currentUser.Username, model.DisplayName, model.WebsiteUrl, model.Description, model.Location).ConfigureAwait(false);
                var viewModel = new ProfileViewModel(updatedProfile);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
