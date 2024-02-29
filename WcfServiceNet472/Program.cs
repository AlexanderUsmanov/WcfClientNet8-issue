using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;

namespace WcfServiceNet472
{
    internal class Program
    {
        private const Int32 Port = 1234;
        private const String EndpointName = "someEndpoint";


        static void Main(string[] args)
        {
            var host = CreateHost();   
            host.Open();

            Console.ReadLine();
        }

        private static ServiceHost CreateHost()
        {
            var localHost = Dns.GetHostEntry(Dns.GetHostName());
            var ip = localHost.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString()).FirstOrDefault();
            var channelAddress = new Uri(new UriBuilder(Uri.UriSchemeNetTcp, ip, Port, EndpointName).ToString(), UriKind.Absolute);

            Console.WriteLine($"Local host IP address is {ip}");
            Console.WriteLine($"Port for WCF: {Port}");
            Console.WriteLine($"Endpoint name: {EndpointName}");

            Console.WriteLine($"Uri: {channelAddress}");

            var binding = new NetTcpBinding("invokeServiceBinding");
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            var service = new ServiceImpl();
            var host = new ServiceHost(service);
            
            host.AddServiceEndpoint(typeof(IService), binding, channelAddress);
            Console.WriteLine($"Starting listening for service IService on address {channelAddress}");

            return host;
        }

    }
}
