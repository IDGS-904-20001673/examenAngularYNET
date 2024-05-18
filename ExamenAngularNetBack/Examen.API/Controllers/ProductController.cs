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
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;

        public ProductController(IWebHostEnvironment env, IProductService productService)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
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
        public async Task<ActionResult<Response<Product>>> CreateProduct([FromForm] ProductRequest request)
        {
            var response = new Response<Product>();

            try
            {
                if (request.Image == null || request.Image.Length == 0)
                {
                    return BadRequest(new { message = "No se subió ninguna imagen" });
                }

                // Verificar que WebRootPath no es nulo
                if (string.IsNullOrEmpty(_env.WebRootPath))
                {
                    throw new InvalidOperationException("WebRootPath no está configurado.");
                }

                // Guardar la imagen en el servidor
                var uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var fileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
                var filePath = Path.Combine(uploadsFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                // Crear el producto con la ruta de la imagen
                var product = new Product
                {
                    Code = request.Code,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock,
                    Img = $"/uploads/{fileName}"
                };

                var newProduct = await _productService.CreateProduct(product);

                response.status = true;
                response.value = newProduct;
                response.message = "SUCCESS";

                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, response);
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
                return StatusCode(500, response);
            }
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
