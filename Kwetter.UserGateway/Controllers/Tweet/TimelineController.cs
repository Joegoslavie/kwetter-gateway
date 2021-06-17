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
    public class TimelineController : KwetterController
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<TimelineController> logger;

        private readonly TweetManager tweetManager;

        public TimelineController(ILogger<TimelineController> logger, TweetManager tweetManager)
        {
            this.logger = logger;
            this.tweetManager = tweetManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeline(string username, int page = 0, int amount = 25)
        {
            try
            {
                var identity = base.GetAuthenticatedUser();
                var tweets = await this.tweetManager.GetRandomTimeline(page, amount).ConfigureAwait(false);
                return Ok(tweets.Select(x => new TweetViewModel(x)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get timeline of the currently signed in user in this context.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("rdm")]
        public async Task<IActionResult> GetRandomTimeline(int page = 1, int amount = 25)
        {
            try
            {
                var tweets = await this.tweetManager.GetRandomTimeline(page, amount).ConfigureAwait(false);
                return Ok(tweets.Select(x => new TweetViewModel(x)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
