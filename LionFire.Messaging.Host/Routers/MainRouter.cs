using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Messaging.Server.Routers
{
    public interface IRouter
    {
        void Receive(object obj);
    }
    public interface IReceiver
    {
    }
    public class MainRouter 
    {
    }
}
