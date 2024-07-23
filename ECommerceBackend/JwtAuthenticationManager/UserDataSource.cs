using JwtAuthenticationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class UserDataSource
    {
        private readonly List<UserAccount> _accounts;
        public UserDataSource()
        {
            _accounts = new List<UserAccount> { new UserAccount { UserName = "admin@gmail.com", Password = "admin123@", Role = "admin" },
                new UserAccount { UserName = "ayush@gmail.com", Password = "ayush123@", Role = "user" }};
        }
        public List<UserAccount> GetAllUserDetails()
        {
            return _accounts;
        }
    }
}
