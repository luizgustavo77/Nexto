namespace Nexto.Commom.Dto.Solicitacao
{
    public class ArquivoSolicitacaoDto : BaseDto
    {
        public SolicitacaoDto Solicitacao { get; set; }
        public ArquivoDto Arquivo { get; set; }
        public string Tipo { get; set; }
    }
}
