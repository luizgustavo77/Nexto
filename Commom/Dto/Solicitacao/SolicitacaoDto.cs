using Commom.Dto.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commom.Dto.Solicitacao
{
    public class SolicitacaoDto : BaseDto
    {
        public string Sigla { get; set; }
        public string Tipo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public UserDto Cliente { get; set; }
        public UserDto Colaborador { get; set; }
        public string Status { get; set; }
        public List<FormularioDto> Formularios { get; set; }
    }
}
