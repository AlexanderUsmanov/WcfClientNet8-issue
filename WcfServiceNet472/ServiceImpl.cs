using System;
using System.ServiceModel;

namespace WcfServiceNet472
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class ServiceImpl : IService
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now} Message received: {message}");
        }
    }
}
