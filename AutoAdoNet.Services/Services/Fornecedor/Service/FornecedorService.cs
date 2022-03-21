using AutoAdoNet.Services.Enum;
using AutoAdoNet.Services.Services.Fornecedor.Dto;
using AutoAdoNet.Services.Services.Fornecedor.Input;
using AutoAdoNet.Services.Services.Fornecedor.Querys;
using AutoAdoNet.Services.Services.Helper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.Fornecedor.Service
{
    public class FornecedorService : IFornecedorService
    {


        #region INJECAO DEPENDENCIA
        private readonly IHelperService _helperService;
        private readonly FornecedorQuerys _querys;

        public FornecedorService(
               IHelperService helperService
               )
        {
            _helperService = helperService;
            _querys = new FornecedorQuerys();
        }
        #endregion

        public async Task<List<FornecedorDto>> Get()
        {

            try
            {
                var dados = _helperService.ExecutaQueryReader<FornecedorDto>(_querys.Select, new FornecedorInput());
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<FornecedorDto>> Get(int Id)
        {

            try
            {
                var dados = _helperService.ExecutaQueryReader<FornecedorDto>(_querys.Select, new FornecedorInput() { Id = Id });
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Insert(FornecedorInput input)
        {

            try
            {
                var dados = _helperService.ExecutaQuery<FornecedorDto>(_querys.Insert, input, QueryType.Insert);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> Update(FornecedorInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<FornecedorDto>(_querys.Update, input, QueryType.Update);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(FornecedorInput input)
        {
            try
            {
                var dados = _helperService.ExecutaQuery<FornecedorDto>(_querys.Delete, input, QueryType.Delete);
                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
