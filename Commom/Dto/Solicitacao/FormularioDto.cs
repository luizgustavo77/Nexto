using System;
using System.Collections.Generic;
using System.Text;

namespace Commom.Dto.Solicitacao
{
    public class FormularioDto : BaseDto
    {
        public SolicitacaoDto Solicitacao { get; set; }
        public string Retorno { get; set; }
        public string CampoAplicacao { get; set; }
        public string FundamentosInvencao { get; set; }
        public string EstadoTecnica { get; set; }
        public string Problemas { get; set; }
        public string Solucaoinvencao { get; set; }
        public string Vantagens { get; set; }
        public string DescricaoDesenhos { get; set; }
        public string DescricaoInvencao { get; set; }
        public List<ArquivoDto> Arquivos { get; set; }
    }
}
