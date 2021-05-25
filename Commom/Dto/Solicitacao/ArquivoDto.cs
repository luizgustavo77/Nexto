namespace Nexto.Commom.Dto.Solicitacao
{
    public class ArquivoDto : BaseDto
    {
        public byte[] Conteudo { get; set; }
        public string Extensao { get; set; }
        public int? Tipo { get; set; }
        public int Solicitacao { get; set; }
    }
}
