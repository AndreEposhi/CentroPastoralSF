using CentroPastoralSF.Api.Application.Dizimista;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.ValueObjects;
using System.Net;
using DizimistaDomain = CentroPastoralSF.Domain.Dizimista.Dizimista;

namespace CentroPastoralSF.Api.Application.Converters
{
    public static class DizimistaConverter
    {
        public static DizimistaDomain ToDizimista(this IncluiDizimistaCommand command)
        {
            var nome = new Nomeacao(command.Nome, command.Sobrenome);
            var telefone = new Telefone(command.Ddd, command.Telefone);
            var endereco = new Endereco(command.Logradouro, command.Numero, command.Complemento,
                command.Bairro, command.Municipio, command.UF, command.Cep);
            var email = new Email(command.Email);

            return new DizimistaDomain(nome, telefone, endereco, email, command.DataNascimento, command.UsuarioId);
        }

        public static DizimistaResponse ToDizimistaResponse(this DizimistaDomain dizimista)
        {
            return new DizimistaResponse 
            {
                Id = dizimista.Id,
                Nome = dizimista.Nome.Nome,
                Sobrenome = dizimista.Nome.Sobrenome,
                Ddd = dizimista.Telefone.Ddd,
                Telefone = dizimista.Telefone.Numero,
                Email = dizimista.Email.Endereco,
                UsuarioId = dizimista.UsuarioId,
                Logradouro = dizimista.Endereco.Logradouro,
                Numero = dizimista.Endereco.Numero,
                Complemento = dizimista.Endereco.Complemento,
                Bairro = dizimista.Endereco.Bairro,
                Municipio = dizimista.Endereco.Municipio,
                UF = dizimista.Endereco.UF,
                Cep = dizimista.Endereco.Cep,
                DataNascimento = dizimista.DataNascimento
            };
        }

        public static Response<IncluiDizimistaResponse> ToIncluiDizimistaResponse(this DizimistaDomain dizimista, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            IncluiDizimistaResponse? data = null;

            if (success)
            {
                data = new IncluiDizimistaResponse
                {
                    Id = dizimista.Id,
                    Nome = dizimista.Nome.Nome,
                    Sobrenome = dizimista.Nome.Sobrenome
                };
            }

            return new Response<IncluiDizimistaResponse>(statusCode, success, data, errors);
        }

        public static Response<AlteraDizimistaResponse> ToAlteraDizimistaResponse(this DizimistaDomain dizimista, HttpStatusCode httpStatus,
            bool success, IEnumerable<string>? errors = null)
        {
            return new Response<AlteraDizimistaResponse>(httpStatus, success, null, errors);
        }

        public static Response<ExcluiDizimistaResponse> ToExcluiDizimistaResponse(this DizimistaDomain dizimista, HttpStatusCode httpStatus,
            bool success, IEnumerable<string>? errors = null)
        {
            return new Response<ExcluiDizimistaResponse>(httpStatus, success, null, errors);
        }

        public static Response<BuscaDizimistaPorIdResponse> ToBuscaDizimistaporIdResponse(this DizimistaDomain dizimista, HttpStatusCode statusCode, 
            bool success, IEnumerable<string>? errors = null)
        {
            BuscaDizimistaPorIdResponse? data = null;

            if (success)
            {
                data = new BuscaDizimistaPorIdResponse
                {
                    Nome = dizimista.Nome.Nome,
                    Sobrenome = dizimista.Nome.Sobrenome,
                    Ddd = dizimista.Telefone.Ddd,
                    Telefone = dizimista.Telefone.Numero,
                    Logradouro = dizimista.Endereco.Logradouro,
                    Numero = dizimista.Endereco.Numero,
                    Complemento = dizimista.Endereco.Complemento,
                    Bairro = dizimista.Endereco.Bairro,
                    Municipio = dizimista.Endereco.Municipio,
                    UF = dizimista.Endereco.UF,
                    Cep = dizimista.Endereco.Cep,
                    Email = dizimista.Email.Endereco,
                    DataNascimento = dizimista.DataNascimento
                };
            }

            return new Response<BuscaDizimistaPorIdResponse>(statusCode, success, data, errors);
        }

        public static Response<IEnumerable<BuscaTodosDizimistasResponse>> ToBuscaTodosDizimistasResponse(this IQueryable<DizimistaDomain> dizimistas, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            var data = new List<BuscaTodosDizimistasResponse>();

            if (success)
            {
                foreach (var dizimista in dizimistas)
                {
                    data.Add(new BuscaTodosDizimistasResponse
                    {
                        Id = dizimista.Id,
                        Nome = dizimista.Nome.Nome,
                        Sobrenome = dizimista.Nome.Sobrenome,
                        Ddd = dizimista.Telefone.Ddd,
                        Telefone = dizimista.Telefone.Numero,
                        Email = dizimista.Email.Endereco
                    });
                }
            }

            return new Response<IEnumerable<BuscaTodosDizimistasResponse>>(statusCode, success, data, errors);
        }
    }
}
