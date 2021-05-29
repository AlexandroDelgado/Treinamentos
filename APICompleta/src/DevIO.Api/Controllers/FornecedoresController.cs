using AutoMapper;
using DevIO.Api.ViewModel;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        // declara o objeto para a inversão de dependência
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        // cria o construtor para a inversão de dependência
        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // Retorna uma lista de fornecedores
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            // Seta um mapeamento (_mapper.Map) de uma lista (IEnumerable<FornecedorViewModel>), recebida de (await _fornecedorRepository.ObterTodos()).
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()); // Lembre-se: recebeu uma lista, mapea uma lista

            // Retorna um código 200 com o ActionResult de fornecedor
            return Ok(fornecedor); // 
        }
    }
}