using Kwetter.UserGateway.VIewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.Controllers
{
    public class KwetterController : ControllerBase
    {
        public KwetterContextUser GetAuthenticatedUser()
        {
            return (KwetterContextUser)HttpContext.Items["User"];
        }
    }
}
