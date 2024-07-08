using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public record BuscaDizimistaPorIdQuery(int Id) : IRequest<Response<BuscaDizimistaPorIdResponse>>;
}
