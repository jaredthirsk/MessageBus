using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging
{
    /// <summary>
    /// Sent to remote clients to indicate the status of a submitted message
    /// </summary>
    public struct MessageResponse
    {
        /// <summary>
        /// Reached the end of the processing without errors
        /// </summary>
        public bool Processed { get; set; }

        /// <summary>
        /// Request completed
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Token for retreiving more info, or matching a response later
        /// </summary>
        public Guid Token {
            get; set;
        }

        public string Data { get; set; }

        //public Exception Exception {
        //    get; set;
        //}
        //public bool IsException {
        //    get { return Exception != null; }
        //}

        //public bool Queued { get; set; }
        //public Guid TaskId { get; set; }
    }
}
