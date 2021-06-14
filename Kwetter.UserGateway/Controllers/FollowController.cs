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
        [Route("follow")]
        public async Task<IActionResult> FollowOrUnfollow([FromBody] FollowViewModel model)
        {
            var user = base.GetAuthenticatedUser();
            bool result = await this.manager.ToggleFollow(user.Id, model.FollowId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Route("followers")]
        public async Task<IActionResult> GetFollowers()
        {
            var user = base.GetAuthenticatedUser();
            var profile = await this.manager.FullProfile(user.Id).ConfigureAwait(false);
            return Ok(profile.Followers);
        }

        [HttpGet]
        [Route("following")]
        public async Task<IActionResult> GetFollowing()
        {
            var user = base.GetAuthenticatedUser();
            var profile = await this.manager.FullProfile(user.Id).ConfigureAwait(false);
            return Ok(profile.Following);
        }
    }
}
