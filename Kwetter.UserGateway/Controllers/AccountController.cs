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
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Logger instance for this controller.
        /// </summary>
        private readonly ILogger<AccountController> logger;


        /// <summary>
        /// Account manager for doing account related operations.
        /// </summary>
        private readonly AccountManager accountManager;

        public AccountController(AccountManager manager, ILogger<AccountController> logger)
        {
            this.accountManager = manager;
            this.logger = logger;
        }
    }
}
