using Music_Portal.BLL.DTO;
using Music_Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.Interfaces
{
    public interface IUserService
    {
        Task TryToRegister(RegisterUserDTO regDto);
        Task<UserDTO> TryToLogin(LoginUserDTO loginDto);
        Task ActivateUser(UserDTO userDto);
        Task BlockUser(UserDTO userDto);
        Task UpdateUser(UserDTO userDto);
        Task DeleteUser(int id);
        Task<UserDTO> GetUser(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IEnumerable<UserDTO>> GetUsersByLogin(string search);
        Task<IEnumerable<UserDTO>> GetAuthorizedUsers();
        Task<IEnumerable<UserDTO>> GetNonAuthorizedUsers();
        Task<IEnumerable<UserDTO>> GetBlockedUsers();
    }
}
