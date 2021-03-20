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

        public override Task<FormularioDto> Find(int Id)
        {
            if (!_ambienteTeste)
                return base.Find(Id);
            else
            {
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
