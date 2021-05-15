using Commom.Dto.Core;
using Commom.Dto.Solicitacao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APIFormulario : APIBase<FormularioDto>
    {
        public APIFormulario(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "http://nextoapiapp.azurewebsites.net/";
            _baseEndpoint = "api/formulario/";
        }

        string texto = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.";

        public override Task<FormularioDto> Find(int Id)
        {
            if (!_ambienteTeste)
                return base.Find(Id);
            else
            {
                FormularioDto formulario = new FormularioDto()
                {
                    Id = 1,
                    CampoAplicacao = texto,
                    DescricaoDesenhos = texto,
                    DescricaoInvencao = texto,
                    Enviado = DateTime.Now,
                    EstadoTecnica = texto,
                    FundamentosInvencao = texto,
                    Problemas = texto,
                    Responsavel = new UserDto()
                    {
                        Id = 1,
                        Nome = "Paulo"
                    },
                    Retorno = "Por favor rever Campo de Aplicacao",
                    Solucaoinvencao = texto,
                    Vantagens = texto,
                    Solicitacao = 1
            };
            return Task.Delay(100).ContinueWith(t => formulario);
        }
    }

    public override Task<List<FormularioDto>> GetAll()
    {
        if (!_ambienteTeste)
            return base.GetAll();
        else
        {
            List<FormularioDto> formularios = new List<FormularioDto>();

            return Task.Delay(100).ContinueWith(t => formularios);
        }
    }
}
}
