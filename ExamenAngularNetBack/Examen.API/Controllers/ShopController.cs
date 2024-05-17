using Examen.BUSSINESS.services.interfaces;
using Examen.BUSSINESS.services;
using Examen.ENTITY;
using Microsoft.AspNetCore.Mvc;
using Examen.API.utilities;

namespace Examen.API.Controllers
{
    [ApiController]
    [Route("shops")]
    public class ShopController : ControllerBase
    {
        private readonly IShopSevice _shopService;

        public ShopController(IShopSevice shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllShops()
        {
            var response = new Response<List<Shop>>();

            try
            {
                response.status=true;
                var shops = await _shopService.GetAllShops();
                response.value = shops.ToList();
                response.message="SUCCESS";
            }
            catch (Exception e)
            {
                response.status=true;
                response.message=e.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {

            var response = new Response<Shop>();

            try
            {
                response.status=true;
                response.value = await _shopService.GetShopById(id);
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
        public async Task<ActionResult<Shop>> CreateShop([FromBody] Shop shop)
        {

            var response = new Response<Shop>();

            try
            {
                response.status=true;
                var newShop = await _shopService.CreateShop(shop);
                CreatedAtAction(nameof(GetShop), new { id = newShop.Id }, newShop);
                response.value = newShop;
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
        public async Task<IActionResult> UpdateShop([FromBody] Shop shop)
        {
            Shop shopU = shop;
            var response = new Response<Shop>();

            try
            {
                var existingShop = await _shopService.GetShopById(shop.Id);
                if (existingShop == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                existingShop.Branch = shopU.Branch;
                existingShop.Address = shopU.Address;

                response.status = true;
                response.value = await _shopService.UpdateShop(existingShop); ;
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
        public async Task<IActionResult> DeleteShop(int id)
        {

            var response = new Response<bool>();


            try
            {
                var shop = await _shopService.GetShopById(id);
                if (shop == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                response.status=true;
                response.value=await _shopService.DeleteShop(id);
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

