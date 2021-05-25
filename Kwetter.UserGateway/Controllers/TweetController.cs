using Kwetter.Business.Manager;
using Kwetter.UserGateway.Attribute;
using Kwetter.UserGateway.VIewModels;
using Kwetter.UserGateway.VIewModels.Tweet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kwetter.UserGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [KwetterAuthorization]
    public class TweetController : KwetterController 
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<TweetController> logger;

        /// <summary>
        /// 
        /// </summary>
        private readonly TweetManager manager;

        public TweetController(ILogger<TweetController> logger, TweetManager manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        [HttpGet]
        [Route("timeline")]
        public async Task<IActionResult> Timeline()
        {
            try
            {
                var identity = base.GetAuthenticatedUser();
                var tweets = await this.manager.GetTimeline(identity.Id);
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                var tweets = await this.manager.GetTweetsByUsername(username).ConfigureAwait(false);
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var tweets = await this.manager.GetTweetsById(userId).ConfigureAwait(false);
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> New([FromBody] NewTweetViewModel model)
        {
            try
            {
                var identity = base.GetAuthenticatedUser();
                var tweet = await this.manager.Place(identity.Id, model.Content).ConfigureAwait(false);
                return Ok(tweet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Like([FromBody] LikeTweetViewModel model)
        {
            try
            {
                var identity = base.GetAuthenticatedUser();
                var result = await this.manager.ToggleLike(identity.Id, model.TweetId).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
