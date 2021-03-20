using Commom.Dto.Solicitacao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APISolicitacao : APIBase<SolicitacaoDto>
    {
        public APISolicitacao(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "https://localhost:44347";
            _baseEndpoint = "API/Solicitacao";
        }

        public override Task<SolicitacaoDto> Find(int Id)
        {
            if (!_ambienteTeste)
                return base.Find(Id);
            else
            {                
                return Task.Delay(100).ContinueWith(t => new SolicitacaoDto());
            }
        }

        public override Task<List<SolicitacaoDto>> GetAll()
        {
            if (!_ambienteTeste)
                return base.GetAll();
            else
            {
                List<SolicitacaoDto> solicitacoes = new List<SolicitacaoDto>();

                return Task.Delay(100).ContinueWith(t => solicitacoes);
            }
        }
    }
}
