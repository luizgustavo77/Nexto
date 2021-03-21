using Commom.Dto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Commom.Dto.Solicitacao
{
    public class FormularioDto : BaseDto
    {
        public SolicitacaoDto Solicitacao { get; set; }

        [DisplayName("Data de envio")]
        public DateTime Enviado { get; set; }
        public UserDto Responsavel { get; set; }

        [DisplayName("Mensagem de retorno")]
        public string Retorno { get; set; }

        [DisplayName("Aplicação")]
        public string CampoAplicacao { get; set; }

        [DisplayName("Fundmentos da invenção")]
        public string FundamentosInvencao { get; set; }

        [DisplayName("Tecnica")]
        public string EstadoTecnica { get; set; }

        public string Problemas { get; set; }

        [DisplayName("Solução")]
        public string Solucaoinvencao { get; set; }
        public string Vantagens { get; set; }

        [DisplayName("Descrição dos desenhos")]
        public string DescricaoDesenhos { get; set; }

        [DisplayName("Descrição da invenção")]
        public string DescricaoInvencao { get; set; }
        public List<ArquivoDto> Arquivos { get; set; }
    }
}
