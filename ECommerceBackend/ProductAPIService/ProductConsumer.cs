


using Common.MessageModels;
using MassTransit;
using ProductAPIService.DataSource;
using ProductAPIService.Models;


namespace ProductAPIService
{
    public class ProductConsumer : IConsumer<GetRequestProduct>
    {
        private readonly IProductData _product;

        private List<Product> _products;
        public ProductConsumer(IProductData product)
        {
            this._product = product;
       
            _products =product.GetProductDetails();

        }
        public async Task Consume(ConsumeContext<GetRequestProduct> context)
        {
            var productId = context.Message.ProductID;
            var product=_products.FirstOrDefault(x=>x.ProductID == productId);
            if(product  == null)
            {
                await context.RespondAsync(new GetResponseProduct { ProductID=0});
            }
            else
            {
                await context.RespondAsync(new GetResponseProduct { ProductID=product.ProductID,ProductName=product.ProductName,ProductDesign=product.ProductDesign,ProductPrice=product.ProductPrice,ProductSize=product.ProductPrice});
            }
           

        }
    }
}
