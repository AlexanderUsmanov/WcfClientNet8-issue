using System;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
namespace WcfClientNet8
{
    internal class Program
    {
        private const Int32 Port = 1234;
        private const String EndpointName = "someEndpoint";

        static void Main(string[] args)
        {
            Console.Write("Enter ip address of server: ");
            var ipAddress = Console.ReadLine();
            Console.Write("Enter user name: ");
            var userName = Console.ReadLine();
            Console.Write("Enter password: ");            
            var password = Console.ReadLine();


            bool ntlmTypeSelected;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Use managed ntlm? [y/n]: ");    
                var response = Console.ReadKey();
                if(response.KeyChar == 'Y' || response.KeyChar == 'y')
                {
                    AppContext.SetSwitch("System.Net.Security.UseManagedNtlm", isEnabled: true);      
                    ntlmTypeSelected = true;
                }
                else if(response.KeyChar == 'N' || response.KeyChar == 'n')
                    ntlmTypeSelected = true;
                else 
                    ntlmTypeSelected = false;
            }
            while(!ntlmTypeSelected);

            var client = CreateClient(ipAddress, userName, password);
            Console.WriteLine("Client created");
            client.SendMessage("Some message");
        }

        private static IService CreateClient(string ipAddress, string userName, string password)
        {
            var channelAddress = new Uri(new UriBuilder(Uri.UriSchemeNetTcp, ipAddress, Port, EndpointName).ToString(), UriKind.Absolute);

            Console.WriteLine($"Client uri: {channelAddress}");

            var binding = new NetTcpBinding
            {
                MaxReceivedMessageSize = Int32.MaxValue,
                SendTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10)
            };
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            var client = new ServiceClient(binding, new EndpointAddress(channelAddress));
            client.ClientCredentials.Windows.ClientCredential = new NetworkCredential(userName, password);
            client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Identification;

            return client;
        }
    }
}