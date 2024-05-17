using Examen.API.utilities;
using Examen.BUSSINESS.services.interfaces;
using Examen.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace Examen.API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = new Response<List<Product>>();

            try
            {
                response.status=true;
                var products = await _productService.GetAllProducts();
                response.value = products.ToList();
                response.message="SUCCESS";
            }
            catch (Exception e){
                response.status=true;
                response.message=e.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            
           var response = new Response<Product>();

            try
            {
                response.status=true;
                response.value = await _productService.GetProductById(id);
                response.message="SUCCESS";
            }
            catch (Exception e)
            {
                response.status=true;
                response.message=e.Message;
            }

            return Ok(response);

        }

        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {

            var response = new Response<Product>();

            try
            {
                response.status=true;
                var newProduct = await _productService.CreateProduct(product);
                CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
                response.value = newProduct;
                response.message="SUCCESS";
            }
            catch (Exception e)
            {
                response.status=true;
                response.message=e.Message;
            }

            return Ok(response);



            
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            Product products= product;
            var response = new Response<Product>();

            try
            {
                var existingProduct = await _productService.GetProductById(products.Id);
                if (existingProduct == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }
                
                existingProduct.Code = products.Code;
                existingProduct.Description = products.Description;
                existingProduct.Price = products.Price;
                if (products.Img == null)
                {
                    existingProduct.Img=existingProduct.Img;
                }
                else {
                    existingProduct.Img = products.Img;
                }
                

                existingProduct.Stock = products.Stock;

                response.status=true; 
                response.value =await _productService.UpdateProduct(existingProduct); ;
                response.message="SUCCESS";
            }
            catch (Exception e)
            {
                response.status=true;
                response.message=e.Message;
            }

            return Ok(response);

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var response = new Response<bool>();
          

            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                response.status=true;
                response.value=await _productService.DeleteProduct(id);
                response.message="SUCCESS";
            }
            catch (Exception e)
            {
                response.status=true;
                response.message=e.Message;
            }

            return Ok(response);
        }
    }
}
