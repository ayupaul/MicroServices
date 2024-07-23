using CartAPIService.Models;

namespace CartAPIService.DataSource
{
    public class CartData
    {
        public List<Cart> _carts;
        public CartData()
        {
            _carts = new List<Cart>();
        }
    }
}
