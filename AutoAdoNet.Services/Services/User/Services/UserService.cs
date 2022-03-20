using AutoAdoNet.Services.Enum;
using AutoAdoNet.Services.Services.Helper.Services;
using AutoAdoNet.Services.Services.User.Dto;
using AutoAdoNet.Services.Services.User.Input;
using AutoAdoNet.Services.Services.User.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.User.Services
{
    public class UserService: IUserService
    {

        #region INJECAO DEPENDENCIA
        private readonly IHelperService _helperService;
        private readonly UserQuerys _querys;

        public UserService(
               IHelperService helperService
               )
        {
            _helperService = helperService;
            _querys = new UserQuerys();
        }
        #endregion

        /// <summary>
        ///  GET All RECORDS  
        /// </summary>
        /// <returns>List of all user records</returns>
        #region GET
        public async Task<List<UserDto>> Get()
        {
            try
            {
                var dados = _helperService.ExecutaQueryReader<UserDto>(_querys.Select, new UserInput());
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        /// <summary>
        /// GEt User BY ID
        /// </summary>
        /// <param name="Id">ID - Nor null</param>
        /// <returns>Return User from ID</returns>
        #region GET WITH PARAMETER
        public async Task<List<UserDto>> Get(int Id)
        {

            try
            {
                var input = new UserInput() { Id = Id };
                var dados = _helperService.ExecutaQueryReader<UserDto>(_querys.Select, input);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        /// <summary>
        /// INSERT user
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        #region INSERT
        public async Task<int> Insert(UserInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<UserDto>(_querys.Insert, input, QueryType.Insert);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion


        /// <summary>
        /// UPDATE User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        #region UPDATE
        public async Task<int> Update(UserInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<UserDto>(_querys.Update, input, QueryType.Update);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// DELETE user
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        #region DELETE
        public async Task<int> Delete(UserInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<UserDto>(_querys.Delete, input, QueryType.Delete);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}
