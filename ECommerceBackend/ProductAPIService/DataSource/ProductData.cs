using ProductAPIService.Models;

namespace ProductAPIService.DataSource
{
    public class ProductData : IProductData
    {
        private List<Product> _products = new List<Product>
        {
            new Product{ProductID=1,ProductName="One Plus Nord 3",ProductDesign="Mobile phone",ProductPrice="34k"},
            new Product{ProductID=2,ProductName="Wrogn Jacket",ProductDesign="Western",ProductPrice="2.5k",ProductSize="XL"}
        };
        public List<Product> GetProductDetails()
        {
            return _products;
        }
    }
}
