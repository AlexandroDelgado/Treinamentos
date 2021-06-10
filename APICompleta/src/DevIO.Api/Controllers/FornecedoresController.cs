using AutoMapper;
using DevIO.Api.ViewModel;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        // declara o objeto para a inversão de dependência
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        // cria o construtor para a inversão de dependência
        public FornecedoresController(IFornecedorService fornecedorService, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // Retorna uma lista de fornecedores
        [HttpGet] // Verbo do método
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            // Seta um mapeamento (_mapper.Map) de uma lista (IEnumerable<FornecedorViewModel>), recebida de (await _fornecedorRepository.ObterTodos()).
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()); // Lembre-se: recebeu uma lista, mapea uma lista

            // Retorna um código 200 com o ActionResult de fornecedor
            return Ok(fornecedor); // 
        }

        [HttpGet("{id:guid}")] // Criando a rota (Verbo) para o fornecedor por id.
        // Retorna o fornecedor por Id
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            // Seta a variavel com o retorno "ObterFornecedorProdutosEndereco", através do mapemento entre a "FornecedorViewModel"
            //  e o repositório do "fornecedor _fornecedorRepository".
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));

            // Verifica se o fornecedor foi preenchido
            if (fornecedor == null) return NotFound(); // Não encontrado

            // Retorna um código 200 com o ActionResult de fornecedor
            return Ok(fornecedor); // 
        }

        // Encapsulando os dados do fornecedor dentro de um método
        public async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            // Retorna os dados do fornecedor, produtos e endereços através do mapeamento entre "FornecedorViewModel" e o "_fornecedorRepository"
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        [HttpGet("{id:guid}")] // Criando a rota (Verbo) para o fornecedor por id.
        // Retornando o fornecedor po Id
        public async Task<ActionResult<FornecedorViewModel>> ObterPorIdEncapsulado(Guid id)
        {
            // Obtem os dados do fornedor através do encapsulamento no método "ObterFornecedorProdutosEndereco".
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            // Verifica se o fornecedor foi preenchido
            if (fornecedor == null) return NotFound(); // Não encontrado

            // retorna o fornecedor
            return Ok(fornecedor);
        }

        [HttpPost]
        // método adicionar fornecedor
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            // Verifica se existe erro no modelo recebido
            if (!ModelState.IsValid) return BadRequest(); // Com erro

            // Mapeaia(converte) o modelo recebido para a entidade fornecedor
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            // Envia o fornecedor para a camada de negócio para ser validado as regras de negócio, ao invé de mandar gravar diretamente no banco.
            var result = await _fornecedorService.Adicionar(fornecedor);

            // Retorna um erro encontrado no FornecedorServiço para a validação da entidade.
            if (!result) return BadRequest();

            // Retorna 
            return Ok(fornecedor);
        }

        [HttpPut("{id:guid}")]
        // método atualizar fornecedor
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            // Verifica se o Id do fornecedor é válido
            if (id != fornecedorViewModel.Id) return BadRequest(); // Com erro

            // Verifica se existe erro no modelo recebido
            if (!ModelState.IsValid) return BadRequest(); // Com erro

            // Mapeaia(converte) o modelo recebido para a entidade fornecedor
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            // Envia o fornecedor para a camada de negócio para ser validado as regras de negócio, ao invés de mandar gravar diretamente no banco.
            var result = await _fornecedorService.Atualizar(fornecedor);

            // Retorna um erro encontrado no FornecedorServiço para a validação da entidade.
            if (!result) return BadRequest();

            // Retorna 
            return Ok(fornecedor);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            // seta o fornecedor
            var fornecedor = await ObterFornecedorEndereco(id);

            // Verifica se o fornecedor foi encontrado
            if (!ModelState.IsValid) return NotFound(); // Não encontrado

            // Remove o fornecedor.
            var result = await _fornecedorService.Remover(id);

            // Verifica se ocorreu algum erro com a remoção do fornecedor através dos serviços da camada de negócio.
            if (!result) return BadRequest();

            // retorna
            return Ok(fornecedor);
        }

        // Encapsulando os dados do fornecedor dentro de um método
        public async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            // Retorna os dados do fornecedor, produtos e endereços através do mapeamento entre "FornecedorViewModel" e o "_fornecedorRepository"
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
    }
}