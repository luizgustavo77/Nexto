using System;
using System.Collections.Generic;
using System.Text;

namespace Commom.Dto.Solicitacao
{
    public class ArquivoDto : BaseDto
    {
        public byte[] Conteudo { get; set; }
        public string Extensao { get; set; }
        public int? Tipo { get; set; }
    }
}
