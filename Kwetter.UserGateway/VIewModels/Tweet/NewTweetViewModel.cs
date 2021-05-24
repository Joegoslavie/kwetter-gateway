using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.UserGateway.VIewModels.Tweet
{
    public class NewTweetViewModel
    {
        [Required]
        public string Content { get; set; }
    }
}
