using Common.MessageModels;
using MassTransit;

namespace CartAPIService.RabbitMQMessageService
{
    public class ProductVerificationService
    {
        private readonly IRequestClient<GetRequestProduct> _clients;
        public ProductVerificationService(IRequestClient<GetRequestProduct> clients)
        {
            _clients = clients;
        }
        public async Task<string> SendVerificationMessage(int id)
        {
            var response = await _clients.GetResponse<GetResponseProduct>(
                   new GetRequestProduct { ProductID = id }
               );
            if (response.Message.ProductID==0)
            {
                return "No product available";
            }
            else
            {
                return "Product found";
            }
        }
    }
}
