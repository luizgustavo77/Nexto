using System;
using System.Collections.Generic;
using System.Text;

namespace Commom.Dto.Solicitacao
{
    public class ArquivoDto : BaseDto
    {
        public byte[] Arquivo { get; set; }
        public string Extensao { get; set; }
    }
}
