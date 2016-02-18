using LionFire.Messaging.Server.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.Application
{
    public class Bootstrapper
    {
        public static MBusAppContext AppContext {
            get; private set;
        }

        public static void Bootstrap(Type startupType = null)
        {
            AppContext = new MBusAppContext();

            InitPipeline(startupType);
        }

        private static void InitPipeline(Type startupType = null)
        {
            AppContext.Pipeline = new Pipeline();

            Type _startupType = startupType;
            if (_startupType == null) { throw new Exception("No startup type found"); }

            object obj = Activator.CreateInstance(_startupType);

            var mi = obj.GetType().GetMethod("ConfigurePipeline", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            if (mi == null)
            {
                throw new Exception("No pipeline configuration found");
            }
            mi.Invoke(obj, new object[] { AppContext.Pipeline });
        }
    }
}
