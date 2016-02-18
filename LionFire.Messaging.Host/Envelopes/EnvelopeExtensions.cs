using LionFire.Messaging.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging
{
    public static class EnvelopeObjectExtensions
    {
        public static T Wrap<T>(this object obj)
            where T : IEnvelope, new()
        {
            var env = new T();
            env.Payload = obj;
            return env;
        }
    }
}
