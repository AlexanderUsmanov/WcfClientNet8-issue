using System.ServiceModel;

namespace WcfServiceNet472
{
    [ServiceContract()]
    public interface IService
    {
        [OperationContract]
        void SendMessage(string message);
    }
}
