using TestApp.DTOs;
using TestApp.Model;

namespace TestApp.Service
{
    public interface IUserService
    {
        bool IsValidUserInformation(UserDto model);
        User GetUserDetails();
    }
}
