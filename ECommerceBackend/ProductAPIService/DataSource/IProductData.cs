using ProductAPIService.Models;

namespace ProductAPIService.DataSource
{
    public interface IProductData
    {
        List<Product> GetProductDetails();
    }
}
