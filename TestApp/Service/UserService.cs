using System.Linq;
using TestApp.Context;
using TestApp.DTOs;
using TestApp.Model;

namespace TestApp.Service
{
    public class UserService : IUserService
    {
        //private readonly IAppDbContext _context;

        //public UserService(IAppDbContext context)
        //{
        //    _context = context;
        //}
        public User GetUserDetails()
        {
            var user = new User
            {
                UserName = "hasan",
                Password = "123456"
            };
            return user;
        }

        //public bool IsValidUserInformation(UserDto model)
        //{
        //    var authorizedUser = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
        //    return authorizedUser != null;
        //}

        public bool IsValidUserInformation(UserDto model)
        {
            return model.UserName.Equals("hasan") && model.Password.Equals("123456");
        }
    }
}
