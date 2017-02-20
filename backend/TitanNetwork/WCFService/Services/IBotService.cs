using System.ServiceModel;
using WCFService.DataTranferObjects;
using WCFService.DataTransferObjects;

namespace WCFService.Services
{
    [ServiceContract]
    public interface IBotService
    {
        [OperationContract]
        string GetResponseFromBot(string message);

        [OperationContract]
        OperationResultDTO SendMessageToBot(MessageDTO message, int chatId);

        [OperationContract]
        OperationResultDTO AttachBotToUser(int userId);
    }
}
