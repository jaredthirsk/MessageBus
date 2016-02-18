using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server
{
    public interface IPipeline
    {
         IPipeline Add(Action<PipelineContext> action);
        IPipeline Add(IPipelineComponent component);

        Task<MessageResponse?> Process(object obj);
    }

    
}
