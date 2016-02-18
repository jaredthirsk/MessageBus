using LionFire.Messaging.Server.Application;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server
{
    [Route("api/[controller]")]
    public class ReceiverController : Controller
    {
        [HttpPost]
        public async Task<MessageResponse?> Post([FromBody]string value)
        {
            return await MBusAppContext.ReceiverMethod(value);
        }
    }
}
