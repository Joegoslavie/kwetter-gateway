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
    public class MentionController : KwetterController
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<MentionController> logger;

        private readonly TweetManager tweetManager;

        public MentionController(ILogger<MentionController> logger, TweetManager tweetManager)
        {
            this.logger = logger;
            this.tweetManager = tweetManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTweetsMentionedIn(string username, int page = 0, int amount = 25)
        {
            try
            {
                var tweets = await this.tweetManager.GetTweetsMentionedIn(username, page, amount).ConfigureAwait(false);
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
