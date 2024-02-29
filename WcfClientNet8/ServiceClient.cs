using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfClientNet8
{
    internal class ServiceClient : ClientBase<IService>, IService
    {
        public ServiceClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now} Sending message: {message}");
            Channel.SendMessage(message);
            Console.WriteLine($"{DateTime.Now} Message sent");
        }
    }
}
