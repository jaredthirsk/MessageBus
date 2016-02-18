using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.Envelopes
{

    public class EnvelopeBase : IEnvelope
    {
        public EnvelopeBase() { }
        public EnvelopeBase(object payload) { this.Payload = payload; }
        public object Payload { get; set; }
    }

}
