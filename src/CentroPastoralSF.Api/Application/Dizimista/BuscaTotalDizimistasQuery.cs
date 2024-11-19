using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public record BuscaTotalDizimistasQuery : IRequest<Response<BuscaTotalDizimistasResponse>>;
}
