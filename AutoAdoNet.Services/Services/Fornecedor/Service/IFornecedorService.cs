using AutoAdoNet.Services.Services.Fornecedor.Dto;
using AutoAdoNet.Services.Services.Fornecedor.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.Fornecedor.Service
{
    public interface IFornecedorService
    {

        Task<List<FornecedorDto>> Get();

        Task<List<FornecedorDto>> Get(int Id);

        Task<int> Insert(FornecedorInput input);

        Task<int> Update(FornecedorInput input);

        Task<int> Delete(FornecedorInput input);

    }
}
