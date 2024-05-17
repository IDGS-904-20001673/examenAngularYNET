using Examen.API.utilities;
using Examen.BUSSINESS.services.interfaces;
using Examen.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = new Response<List<User>>();

            try
            {
                response.status=true;
                var users = await _UserService.GetAllUsers();
                response.value = users.ToList();
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
        public async Task<ActionResult<User>> GetUser(int id)
        {

            var response = new Response<User>();

            try
            {
                response.status=true;
                response.value = await _UserService.GetUserById(id);
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
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {

            var response = new Response<User>();

            try
            {
                response.status=true;
                var newUser = await _UserService.CreateUser(user);
                CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
                response.value = newUser;
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
        [Route("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] LoginDTO user)
        {

            var response = new Response<User>();

            try
            {
                response.status=true;
                response.value = await _UserService.ValidateUser(user.email, user.password);
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
        public async Task<IActionResult> UpdateShop([FromBody] User user)
        {
            User userU = user;
            var response = new Response<User>();

            try
            {
                var existingUser = await _UserService.GetUserById(user.Id);
                if (existingUser == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                existingUser.Address = userU.Address ;
                existingUser.Name = userU.Name;
                existingUser.LastName = userU.LastName;
                existingUser.Users = userU.Users;
                existingUser.Passsword = userU.Passsword;

                response.status = true;
                response.value = await _UserService.UpdateUser(existingUser); ;
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
        public async Task<IActionResult> DeleteUser(int id)
        {

            var response = new Response<bool>();


            try
            {
                var shop = await _UserService.GetUserById(id);
                if (shop == null)
                {
                    response.status=false;
                    response.message="NOT FOUND";
                }

                response.status=true;
                response.value=await _UserService.DeleteUser(id);
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
