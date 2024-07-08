using CentroPastoralSF.Api.Application.Dizimista;
using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Utilities;

namespace CentroPastoralSF.Api.Converters
{
    public static class DizimistaConverter
    {
        public static IncluiDizimistaCommand ToIncluiDizimistaCommand(this IncluiDizimistaRequest request)
        {
            request.Telefone = request.Telefone.RemoverFormatacaoTelefone();
            request.Cep = request.Cep.RemoverFormatacaoCep();

            return new IncluiDizimistaCommand(request.Nome, request.Sobrenome, request.Ddd, request.Telefone,
                request.Logradouro, request.Numero, request.Complemento, request.Bairro, request.Municipio,
                request.UF, request.Cep, request.UsuarioId, request.Email, request.DataNascimento);
        }

        public static AlteraDizimistaCommand ToAlteraDizimistaCommand(this AlteraDizimistaRequest request)
        {
            request.Telefone = request.Telefone.RemoverFormatacaoTelefone();
            request.Cep = request.Cep.RemoverFormatacaoCep();

            return new AlteraDizimistaCommand(request.Id, request.Nome, request.Sobrenome, request.Ddd,
                request.Telefone, request.Logradouro, request.Numero, request.Complemento,
                request.Bairro, request.Municipio, request.UF, request.Cep, request.UsuarioId,
                request.Email,request.DataNascimento); 
        }
    }
}