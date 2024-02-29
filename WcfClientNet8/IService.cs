using System.ServiceModel;

namespace WcfClientNet8
{
    [ServiceContract()]
    public interface IService
    {
        [OperationContract]
        void SendMessage(string message);
    }
}
