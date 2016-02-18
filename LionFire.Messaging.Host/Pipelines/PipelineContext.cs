using LionFire.Messaging.Server;
using LionFire.Messaging.Server.Envelopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging
{
    public class PipelineContext : EnvelopeBase
    {

        #region MetaData

        public Dictionary<string, object> Properties {
            get {
                if (properties == null) { properties = new Dictionary<string, object>(); }
                return properties;
            }
            set { properties = value; }
        }
        private Dictionary<string, object> properties;

        #endregion

        public MessageResponse? Result { get; set; }
        public bool ContinuePipeline { get; set; } = true;

        public bool ShouldLogMessage { get; set; } 
    }
}
