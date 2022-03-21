using AutoAdoNet.Services.Error.Dto;
using AutoAdoNet.Services.Services.Fornecedor.Input;
using AutoAdoNet.Services.Services.Fornecedor.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAdoNet.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private IFornecedorService _fornecedorService;
        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        /// <summary>
        /// Retorna todos os usuarios cadastrados
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Usuarios cadastrados</returns>
        /// <response code="200">Retorna todos os usuarios cadastrados.</response>
        /// <response code="400">Caso não encontre nenhum usuario cadastrado.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _fornecedorService.Get();
            if (result.Result.Count <= 0)
            {
                return NotFound(new ErrorDto() { ErrorMessage = "Não foi possivel localizar nenhum registro." });
            }
            return Ok(result);
        }


        /// <summary>
        /// Retorna fornecedor cadastrado usando pesquisando pelo ID
        /// </summary>
        /// <param name="Id">ID do usuario</param>
        /// <returns>Retorna fornecedor cadastrado usando pesquisando pelo ID</returns>
        /// <response code="200">Retorna todos os fornecedores cadastrados.</response>
        /// <response code="400">Caso não encontre nenhum fornecedor cadastrado.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _fornecedorService.Get(id);
            if (result.Result.Count <= 0)
            {
                return NotFound(new ErrorDto() { ErrorMessage = "Não foi possivel localizar nenhum registro." });
            }
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(FornecedorInput input)
        {
            try
            {
                var resultado = await _fornecedorService.Insert(input);
                if (resultado > 0)
                {
                    input.Id = resultado;
                    return CreatedAtAction("Get", new { Id = resultado }, input);
                }
                else
                {
                    return BadRequest(new ErrorDto() { ErrorMessage = "Não foi possivel gravar!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDto() { ErrorMessage = ex.Message });
            }



        }
        [HttpPut]
        public async Task<IActionResult> Update(FornecedorInput input)
        {
            try
            {
                if (!input.Id.HasValue)
                {
                    return BadRequest(new ErrorDto() { ErrorMessage = "Campo ID não pode ficar em branco" });
                }
                var resultado = await _fornecedorService.Update(input);
                if (resultado > 0)
                {
                    return Ok(input);
                }
                else
                {
                    return BadRequest(new ErrorDto() { ErrorMessage = "Não foi possivel gravar!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FornecedorInput input)
        {
            try
            {
                if (!input.Id.HasValue)
                {
                    return BadRequest(new ErrorDto() { ErrorMessage = "Campo ID não pode ficar em branco" });
                }
                var resultado = await _fornecedorService.Delete(input);
                if (resultado > 0)
                {
                    return Ok(input);
                }
                else
                {
                    return BadRequest(new ErrorDto() { ErrorMessage = "Não foi possivel deletar!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
