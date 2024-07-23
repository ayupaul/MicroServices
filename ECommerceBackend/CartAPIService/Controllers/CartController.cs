
using CartAPIService.DataSource;
using CartAPIService.Models;
using CartAPIService.RabbitMQMessageService;



using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace CartAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

       
        private readonly ProductVerificationService _productVerification;
        private readonly CartData _cartData;
        private List<Cart> _carts;
        //private List<Cart> _carts=new List<Cart>();
        public CartController(ProductVerificationService productVerification,CartData cartData)
        {
           
            
            this._productVerification = productVerification;
            this._cartData = cartData;
            _carts = cartData._carts;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            var productVerification = await _productVerification.SendVerificationMessage(cart.ProductID);
            if (productVerification == "Product found")
            {
                _carts.Add(cart);
                return Ok(cart);
            }
            else
            {
                return NotFound("Product is not available");
            }


        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_carts);
            
        }
    }
}
