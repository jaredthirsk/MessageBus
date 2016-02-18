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

    public class LogMessageToFileOptions : MessageLogOptions
    {
        public string File { get; set; }
    }

    
    public class LogMessageToFile : IPipelineComponent, IDisposable
    {
        public LogMessageToFileOptions Options {
            get; set;
        }

        FileStream fileStream;
        StreamWriter writer;

        #region Construction and Destruction

        public void Init()
        {
            if (fileStream != null)
            {
                return;
            }
            fileStream = new FileStream(Options.File, FileMode.Append);
            writer = new StreamWriter(fileStream);
            writer.AutoFlush = true;
        }

        public void Dispose()
        {
            var fs = fileStream;
            if (fs != null)
            {
                fileStream = null;
                writer.Flush();
                writer.Dispose();
                writer = null;
                fs.Dispose();
            }
        }

        #endregion

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
                writer.WriteLine(JsonConvert.SerializeObject(eLog));
                writer.FlushAsync();
            }
            );
        }
    }

    public static class LogMessageToFileExtensions
    {
        public static IPipeline LogMessageToFile(this IPipeline pipeline, string path)
        {
            var obj = new LogMessageToFile();

            var dir = Path.GetDirectoryName(path);
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

            obj.Options = new LogMessageToFileOptions()
            {
                File = path
            };
            obj.Init();
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
//    var queueFile = Path.Combine(AppConfig.DataRoot, "queue");

//    var enqueueDate = DateTime.UtcNow;
//    var fileName = enqueueDate.ToString("yyyy.MM.dd_hh-mm-ss.ffff") + ".json";

//    var env = new QueueEnvelope()
//    {
//    };

//    using (var fs = new FileStream(Path.Combine(queueFile, fileName), FileMode.CreateNew))
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

