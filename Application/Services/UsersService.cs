using App.Exceptions;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class UsersService
    {
        private readonly IRepository<User> userRepository;

        public UsersService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public string Login(string username, string password)
        {            
            if (string.IsNullOrEmpty(username.Trim()))
                return "Не указан пользователь!";
            if (string.IsNullOrEmpty(password.Trim()))
                return "Не указан пароль!";

            var user = userRepository.GetAll(p=>p.Username == username).FirstOrDefault();
            if(user == null)
            {
                return "Пользователь " + username + " не найден!";
            }
            if (user.Password!=password)
            {
                return "Неверный пароль!";
            }
            return null;
        }
    }
}
