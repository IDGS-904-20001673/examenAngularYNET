using Examen.BUSSINESS.services.interfaces;
using Examen.DATA.Repositories.interfaces;
using Examen.ENTITY;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.BUSSINESS.services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository=userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.Insert(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = await _userRepository.GetOne(u => u.Id == id);
            if (userToDelete == null)
                return false;

            return await _userRepository.Delete(userToDelete);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetOne(u => u.Id == userId);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.Update(user);
        }

        public async Task<User> ValidateUser(string correo, string clave)
        {
            try
            {
                var queryUser = await _userRepository.GetAll(u =>
                u.Users == correo && u.Passsword == clave);

                if (queryUser.FirstOrDefault() == null) {
                    throw new TaskCanceledException("No existe");
                }
                User returnUser = queryUser.FirstOrDefault();
                return returnUser;
            }
            catch {
                throw new TaskCanceledException("No existe");
            }
        }

      
    }
}
