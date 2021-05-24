using Kwetter.Business.Manager;
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
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<ProfileController> logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult View()
        {
            return null;
        }
    }
}
