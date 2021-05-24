using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels
{
    [DebuggerDisplay("id {Id} | {Username}")]
    public class KwetterContextUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
