using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server
{
    [Route("api/[controller]")]
    public class StatusController
    {
        [HttpGet]
        public string Get()
        {
            return "{status:'running-hardcode'}";
        }
    }
}
