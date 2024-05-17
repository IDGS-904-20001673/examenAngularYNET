using Examen.API.utilities;
using Examen.BUSSINESS.services.interfaces;
using Examen.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.API.Controllers
{
    [ApiController]
    [Route("UserProduct")]
    public class UserProductController : Controller
    {
        private readonly IUserProductService _userProductService;


        public UserProductController(IUserProductService userProductService)
        {
            _userProductService = userProductService;

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUserProduct()
        {
            var response = new Response<List<UserProduct>>();

            try
            {
                response.status = true;

                // Obtener los ProductShop con las entidades relacionadas cargadas
                var userProduct = await _userProductService.GetAllUserProduct();

                // Mapear los datos necesarios para la respuesta
                var mappedUserProduct = userProduct.Select(up => new UserProduct
                {
                    Id = up.Id,
                    IdCustomer=up.IdCustomer,
                    IdProduct=up.IdProduct,
                    IdProductNavigation = new Product
                    {
                        Id = up.IdProductNavigation.Id,
                        Code = up.IdProductNavigation.Code,
                        Description = up.IdProductNavigation.Description,
                        Price = up.IdProductNavigation.Price,
                        Img = up.IdProductNavigation.Img,
                        Stock = up.IdProductNavigation.Stock
                    },
                    IdCustomerNavigation = new User
                    {
                        Id = up.IdCustomerNavigation.Id,
                        Name = up.IdCustomerNavigation.Name,
                        LastName = up.IdCustomerNavigation.LastName,
                        Address = up.IdCustomerNavigation.Address,
                        Users = up.IdCustomerNavigation.Users,

                    }
                }).ToList();


                response.value = mappedUserProduct;
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
        public async Task<ActionResult<UserProduct>> GetUserProduct(int id)
        {

            var response = new Response<UserProduct>();

            try
            {
                response.status=true;
                response.value = await _userProductService.GetUserProductById(id);
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
        public async Task<ActionResult<UserProduct>> CreateUserProduct([FromBody] UserProduct userProduct)
        {

            var response = new Response<UserProduct>();

            try
            {
                response.status=true;
                var newUserProduct = await _userProductService.CreateUserProduct(userProduct);
                CreatedAtAction(nameof(GetUserProduct), new { id = newUserProduct.Id }, newUserProduct);
                response.value = newUserProduct;
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
        public async Task<IActionResult> UpdateProductShop([FromBody] UserProduct userProduct)
        {
            UserProduct userProductU = userProduct;
            var response = new Response<UserProduct>();

            try
            {
                var existingUserProduct = await _userProductService.GetUserProductById(userProduct.Id);
                if (existingUserProduct == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                existingUserProduct.IdProduct = userProductU.IdProduct;
                existingUserProduct.IdCustomer = userProductU.IdCustomer;

                response.status = true;
                response.value = await _userProductService.UpdateUserProduct(existingUserProduct); ;
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
        public async Task<IActionResult> DeleteUserProduct(int id)
        {

            var response = new Response<bool>();


            try
            {
                var shop = await _userProductService.GetUserProductById(id);
                if (shop == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                response.status=true;
                response.value=await _userProductService.DeleteUserProduct(id);
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
