using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.Dizimista;
using CentroPastoralSF.Domain.ValueObjects;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public class AlteraDizimistaCommandHandler(IDizimistaService dizimistaService) : IRequestHandler<AlteraDizimistaCommand, Response<AlteraDizimistaResponse>>
    {
        public async Task<Response<AlteraDizimistaResponse>> Handle(AlteraDizimistaCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var dizimista = await dizimistaService.BuscarPorId(command.Id);

                if (dizimista is null)
                {
                    erros.Add("Dizimista não existe.");

                    return new Response<AlteraDizimistaResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                //Todo: Levar para dizimista service
                var nome = new Nomeacao(command.Nome, command.Sobrenome);
                var telefone = new Telefone(command.Ddd, command.Telefone);
                var endereco = new Endereco(command.Logradouro, command.Numero, command.Complemento,
                    command.Bairro, command.Municipio, command.UF, command.Cep);
                var email = new Email(command.Email);

                dizimista.Atualizar(nome, telefone, endereco, email, command.DataNascimento.Date, command.UsuarioId);

                var validacoesCampos = dizimista.Validacao.Erros.Where(d => d.TipoValidacao == Domain.TipoValidacao.Campo);
                var validacoesNegocios = dizimista.Validacao.Erros.Where(d => d.TipoValidacao == Domain.TipoValidacao.Negocio);

                if (validacoesCampos.Any())
                {
                    foreach (var erro in validacoesCampos)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return dizimista.ToAlteraDizimistaResponse(HttpStatusCode.BadRequest, false, errors: erros);
                }

                if (validacoesNegocios.Any())
                {
                    foreach (var erro in validacoesNegocios)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return dizimista.ToAlteraDizimistaResponse(HttpStatusCode.UnprocessableEntity, false, errors: erros);
                }

                await dizimistaService.Alterar(dizimista);

                return dizimista.ToAlteraDizimistaResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao atualizar o dizimista: {ex.Message}");

                return new Response<AlteraDizimistaResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}