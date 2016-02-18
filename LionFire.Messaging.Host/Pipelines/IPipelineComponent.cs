using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server
{
    public interface IPipelineComponent
    {
        void Process(PipelineContext ctx);
    }
}
