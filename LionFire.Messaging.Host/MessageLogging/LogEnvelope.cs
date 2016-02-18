using LionFire.Messaging.Server.Envelopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.MessageLogging
{
    public class LogEnvelope : EnvelopeBase
    {
        #region Construction

        public LogEnvelope() { }
        public LogEnvelope(object payload) : base(payload) { }

        #endregion

        public Dictionary<string, object> Properties;
    }

    public static class LogEnvelopeExtensions
    {
        public static void AddHostName(this LogEnvelope env, PipelineContext ctx = null)
        {
            if (env.Properties == null)
            {
                env.Properties = new Dictionary<string, object>();
            }

            var p = env.Properties;

#if DNX451
            p.Add("MachineName", Environment.MachineName);
#elif DNXCORE50
            p.Add("MachineName", "?");
#else
#endif
            if (ctx != null)
            {

#if DNX451
#elif DNXCORE50
#else
#endif
            }
        }

        public static void AddRuntimeInfo(this LogEnvelope env)
        {
            if (env.Properties == null)
            {
                env.Properties = new Dictionary<string, object>();
            }

            var p = env.Properties;

            p.Add("ThreadId", Environment.CurrentManagedThreadId);

#if DNX451
                p.Add("RuntimePlatform", "DNX451");
                p.Add("EntryAssembly", Assembly.GetEntryAssembly()?.Location);
                p.Add("OSVersion", Environment.OSVersion);
#elif DNXCORE50
            p.Add("RuntimePlatform", "DNXCORE50");
            //p.Add("GetExecutingAssembly", Assembly.GetExecutingAssembly().Location);
#else
                p.Add("RuntimePlatform", "(Unknown)");
#endif
        }

        public static void AddExceptionInfo(this LogEnvelope env, Exception ex = null)
        {
            if (env.Properties == null)
            {
                env.Properties = new Dictionary<string, object>();
            }

            var p = env.Properties;

            if (ex != null)
            {
                p.Add("ExceptionMessage", ex.Message);
                p.Add("ExceptionStackTrace", ex.StackTrace);

#if DNX451
#elif DNXCORE50
#else
#endif
            }
        }
    }
}
