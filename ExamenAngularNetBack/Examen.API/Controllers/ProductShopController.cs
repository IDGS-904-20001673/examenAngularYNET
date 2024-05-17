using Examen.API.utilities;
using Examen.BUSSINESS.services.interfaces;
using Examen.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace Examen.API.Controllers
{
    [ApiController]
    [Route("productShops")]
    public class ProductShopController : ControllerBase
    {
        private readonly IProductShopService _productShopService;


        public ProductShopController(IProductShopService productShopService)
        {
            _productShopService = productShopService;

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllProductShops()
        {
            var response = new Response<List<ProductShop>>();

            try
            {
                response.status = true;

                // Obtener los ProductShop con las entidades relacionadas cargadas
                var productShops = await _productShopService.GetAllProductShops();

              // Mapear los datos necesarios para la respuesta
                var mappedProductShops = productShops.Select(ps => new ProductShop
                {
                Id = ps.Id,
                IdProduct=ps.IdProduct,
                IdShop=ps.IdShop,
                Date = ps.Date,
                IdProductNavigation = new Product
                {
                    Id = ps.IdProductNavigation.Id,
                    Code = ps.IdProductNavigation.Code,
                    Description = ps.IdProductNavigation.Description,
                    Price = ps.IdProductNavigation.Price,
                    Img = ps.IdProductNavigation.Img,
                    Stock = ps.IdProductNavigation.Stock
                },
                IdShopNavigation = new Shop
                {
                    Id = ps.IdShopNavigation.Id,
                    Branch = ps.IdShopNavigation.Branch,
                    Address = ps.IdShopNavigation.Address
                }
                }).ToList();


                response.value = mappedProductShops;
                response.message = "SUCCESS";
            }
            catch (Exception e)
            {
                response.status = false;
                response.message = e.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<ActionResult<ProductShop>> GetProductShop(int id)
        {

            var response = new Response<ProductShop>();

            try
            {
                response.status=true;
                response.value = await _productShopService.GetProductShopById(id);
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
        public async Task<ActionResult<ProductShop>> CreateProductShop([FromBody] ProductShop productShop)
        {

            var response = new Response<ProductShop>();

            try
            {
                response.status=true;
                productShop.Date=DateTime.Now;
                var newProductShop = await _productShopService.CreateProductShop(productShop);
                CreatedAtAction(nameof(GetProductShop), new { id = newProductShop.Id }, newProductShop);
                response.value = newProductShop;
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
        public async Task<IActionResult> UpdateProductShop([FromBody] ProductShop productShop)
        {
            ProductShop productShopU = productShop;
            var response = new Response<ProductShop>();

            try
            {
                var existingProductShop = await _productShopService.GetProductShopById(productShop.Id);
                if (existingProductShop == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                existingProductShop.IdProduct = productShopU.IdProduct;
                existingProductShop.IdShop = productShopU.IdShop;

                response.status = true;
                response.value = await _productShopService.UpdateProductShop(existingProductShop); ;
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
        public async Task<IActionResult> DeleteProductShop(int id)
        {

            var response = new Response<bool>();


            try
            {
                var shop = await _productShopService.GetProductShopById(id);
                if (shop == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                response.status=true;
                response.value=await _productShopService.DeleteProductShop(id);
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
