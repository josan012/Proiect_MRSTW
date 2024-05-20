/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<OperationDetails> DeleteUserByUsername(string username);
        Task<UserDTO> GetUserById(string userId);
        Task<OperationDetails> UpdateClient(UserDTO user);
        Task<UserDTO> GetUserByUsername(string username);

    }
}
*/