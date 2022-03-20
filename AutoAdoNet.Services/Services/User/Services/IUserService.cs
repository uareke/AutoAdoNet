using AutoAdoNet.Services.Services.User.Dto;
using AutoAdoNet.Services.Services.User.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.User.Services
{
    public interface IUserService
    {

        Task<List<UserDto>> Get();

        Task<List<UserDto>> Get(int Id);

        Task<int> Insert(UserInput input);

        Task<int> Update(UserInput input);

        Task<int> Delete(UserInput input);

    }
}
