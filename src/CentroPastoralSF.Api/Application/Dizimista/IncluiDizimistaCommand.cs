using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public record IncluiDizimistaCommand(string Nome, string Sobrenome, string Ddd, string Telefone,
        string Logradouro, string Numero, string Complemento, string Bairro, string Municipio,
        string UF, string Cep, int UsuarioId, string Email, DateTime DataNascimento) : IRequest<Response<IncluiDizimistaResponse>>;
}
