using AutoMapper;
using DevIO.Api.ViewModel;
using DevIO.Business.Models;

namespace DevIO.Api.Configurations
{
    // Essa classe herda a classe Profile, pertencente ao Automapper
    public class AutomapperConfig : Profile
    {
        // Contrutor da configuração de mapeamento
        public AutomapperConfig()
        {
            // Faz o mapeamento das classes
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap(); // Caso não seja necessário usar parametros, utilize o ".ReversMap()".
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
