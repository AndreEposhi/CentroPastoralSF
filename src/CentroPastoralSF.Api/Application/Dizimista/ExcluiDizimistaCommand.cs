using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public record ExcluiDizimistaCommand(int Id) : IRequest<Response<ExcluiDizimistaResponse>>;
}
