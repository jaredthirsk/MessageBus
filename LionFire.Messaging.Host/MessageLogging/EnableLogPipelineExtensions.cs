using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server
{

    
    public static class EnableLogPipelineExtensions
    {

        #region EnableLog

        /// <summary>
        /// Place this before any Log members in the pipeline, as the Log members will check the ShouldLogMessage flag
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IPipeline EnableLog(this IPipeline pipeline, Predicate<PipelineContext> where = null)
        {
            if (where != null)
            {
                var w = new _EnableLogWhere(where);
                pipeline.Add(w.Process);
            }
            else {
                pipeline.Add(_EnableLogAlways);
            }
            return pipeline;
        }

        #region (Private)

        private class _EnableLogWhere
        {
            Predicate<PipelineContext> where;
            public _EnableLogWhere(Predicate<PipelineContext> where)
            {
                this.where = where;
            }
            public void Process(PipelineContext ctx)
            {
                if (where(ctx)) { ctx.ShouldLogMessage = true; }
            }
        }
        private static void _EnableLogAlways(PipelineContext ctx)
        {
            ctx.ShouldLogMessage = true;
        }

        #endregion

        #endregion

    }
}
