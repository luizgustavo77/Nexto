using Commom.Dto.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Commom.Dto.Solicitacao
{
    public class SolicitacaoDto : BaseDto
    {
        public string Sigla { get; set; }
        public int? Tipo { get; set; }

        [DisplayName("Data de inicio")]
        public DateTime? DataInicio { get; set; }

        [DisplayName("Data final")]
        public DateTime? DataFim { get; set; }
        public UserDto Cliente { get; set; }
        public UserDto Colaborador { get; set; }
        public int? Status { get; set; }
        public List<FormularioDto> Formularios { get; set; }
    }
}
