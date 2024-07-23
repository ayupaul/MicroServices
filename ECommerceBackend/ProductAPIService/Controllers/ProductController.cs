
using Microsoft.AspNetCore.Mvc;
using ProductAPIService.DataSource;
using ProductAPIService.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _product;
        private  List<Product> _products;
        public ProductController(IProductData product)
        {
            this._product = product;
            _products=_product.GetProductDetails();
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            if(_products.Count == 0)
            {
                return NotFound();
            }
            return Ok(_products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product= _products.FirstOrDefault(it => it.ProductID == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            product.ProductID = _products.Count()+1;
            _products.Add(product);
            return Ok(product);
        }

       

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product=_products.FirstOrDefault(it => it.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return Ok(product);
        }
        

    }
}
