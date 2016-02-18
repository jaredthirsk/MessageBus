using LionFire.Messaging.Server.Envelopes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.Pipelines
{
 
    public class Pipeline : IPipeline
    {
        private List<Action<PipelineContext>> pipelineActionSequence = new List<Action<PipelineContext>>();

        public IPipeline Add(IPipelineComponent processor)
        {
            pipelineActionSequence.Add(processor.Process);
            return this;
        }
        public IPipeline Add(Action<PipelineContext> processMethod)
        {
            pipelineActionSequence.Add(processMethod);
            return this;
        }

        public async Task<MessageResponse?> Process(object obj)
        {
            var ctx = obj.Wrap<PipelineContext>();

            foreach (var action in pipelineActionSequence)
            {
                await Task.Run(() => action(ctx));
                if (!ctx.ContinuePipeline) break;
            }

            ctx.Result = new MessageResponse() { Processed = true };
            return ctx.Result;
        }

        public async Task<MessageResponse?> Process(string str)
        {
            object obj;
            obj = str == null ? null : JsonConvert.DeserializeObject(str);
            var response = await Process(obj);
            return response;
        }

    }
}
