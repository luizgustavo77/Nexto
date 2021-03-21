using Commom.Dto.Solicitacao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APIFormulario : APIBase<FormularioDto>
    {
        public APIFormulario(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "https://localhost:44347";
            _baseEndpoint = "API/Formulario";
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
                    CampoAplicacao = ""
                };
                return Task.Delay(100).ContinueWith(t => new FormularioDto());
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
