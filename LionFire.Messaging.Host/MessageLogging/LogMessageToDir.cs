using LionFire.Messaging.Server.Envelopes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.MessageLogging
{
  
    public class LogMessageToDirOptions : MessageLogOptions
    {
        public string Dir { get; set; }
    }

    public class LogMessageToDir : IPipelineComponent
    {
        public LogMessageToDirOptions Options {
            get; set;
        }

        public void Process(PipelineContext ctx)
        {
            var obj = ctx.Payload;
            var enqueueDate = DateTime.UtcNow;

            var eLog = new LogEnvelope()
            {
                Payload = obj
            };
            eLog.AddHostName(ctx);
            eLog.AddRuntimeInfo(); // TEMP - verbose

            var task = Task.Factory.StartNew(() =>
            {
                var fileName = enqueueDate.ToString("yyyy.MM.dd_hh-mm-ss.ffff") + ".json";

                using (var fs = new FileStream(Path.Combine(Options?.Dir ?? "", fileName), FileMode.CreateNew))
                {
                    using (var tw = new StreamWriter(fs))
                    {
                        tw.Write(JsonConvert.SerializeObject(eLog));
                    }
                }
            }
            );
        }
    }
    public static class LogMessageToDirExtensions
    {
        public static IPipeline LogMessageToDir(this IPipeline pipeline, string dir)
        {
            var obj = new LogMessageToDir();

            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch
                {
                    // EMPTYCATCH
                }
            }

            obj.Options = new LogMessageToDirOptions()
            {
                Dir = dir
            };

            pipeline.Add(obj);
            return pipeline;
        }
    }
}

//public class QueueEnvelope : EnvelopeBase
//{
//    public string MachineName { get; set; }
//    public DateTime ReceiveTime { get; set; }
//}
//private async void Queue(object obj)
//{
//    var queueDir = Path.Combine(AppConfig.DataRoot, "queue");

//    var enqueueDate = DateTime.UtcNow;
//    var fileName = enqueueDate.ToString("yyyy.MM.dd_hh-mm-ss.ffff") + ".json";

//    var env = new QueueEnvelope()
//    {
//    };

//    using (var fs = new FileStream(Path.Combine(queueDir, fileName), FileMode.CreateNew))
//    {
//        using (var tw = new StreamWriter(fs))
//        {
//            //new Newtonsoft.Json.Bson.BsonWriter(fs);
//            //    var j = new Newtonsoft.Json.JsonTextWriter(tw);
//            //j.Write
//            await Task.Factory.StartNew(() => tw.Write(JsonConvert.SerializeObject(obj)));
//        }
//    }
//}

