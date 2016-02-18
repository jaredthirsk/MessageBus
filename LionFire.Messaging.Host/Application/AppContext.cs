using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.Application
{
    public class MBusAppContext
    {
        #region Construction

        private static MBusAppContext instance;
        public MBusAppContext()
        {
            if (instance != null) throw new NotSupportedException("AppContext can only be created once.");
            instance = this;

            ReceiverMethod = defaultAsyncReceiverMethod;
        }

        #endregion

        #region Pipeline

        public IPipeline Pipeline {
            get { return sPipeline; }
            set { sPipeline = value; }
        }
        private static IPipeline sPipeline;
        public static IPipeline SPipeline {
            get { return sPipeline; }
        }

        #endregion

        public static Func<string, Task<MessageResponse?>> ReceiverMethod;

        //public static async ReceiverResult? defaultReceiverMethod(string value)  OLD
        //{
        //    var ctx = await sPipeline.Process(value);
        //    return ctx.Result;
        //}
        public static async Task<MessageResponse?> defaultAsyncReceiverMethod(string value)
        {
            var response = await sPipeline.Process(value);
            return response;
        }

    }
}
