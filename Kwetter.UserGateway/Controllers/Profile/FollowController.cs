using Kwetter.Business.Manager;
using Kwetter.UserGateway.Attribute;
using Kwetter.UserGateway.VIewModels.Follow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [KwetterAuthorization]
    public class FollowController : KwetterController
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<FollowController> logger;

        private readonly FollowManager manager;

        public FollowController(ILogger<FollowController> logger, FollowManager manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        [HttpPost]
        [Route("toggle")]
        public async Task<IActionResult> FollowOrUnfollow([FromBody] FollowViewModel model)
        {
            var user = base.GetAuthenticatedUser();
            return Ok(await this.manager.ToggleFollow(user.Id, model.Username).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("followers")]
        public async Task<IActionResult> GetFollowers(string username, int page = 1, int amount = 1)
        {
            var user = base.GetAuthenticatedUser();
            var profile = await this.manager.FullProfile(user.Id).ConfigureAwait(false);
            return Ok(profile.Followers);
        }

        [HttpGet]
        [Route("following")]
        public async Task<IActionResult> GetFollowing(string username, int page = 1, int amount = 1)
        {
            var user = base.GetAuthenticatedUser();
            var profile = await this.manager.FullProfile(user.Id).ConfigureAwait(false);
            return Ok(profile.Following);
        }
    }
}
