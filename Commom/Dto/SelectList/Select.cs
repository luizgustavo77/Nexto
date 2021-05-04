using Commom.Proxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commom.Dto.SelectList
{
    public class Select
    {
        public async Task CarregaDados()
        {
            await new APISelect(false).Iniciar();
        }

        public static List<Option> Sexo = new List<Option>();

        public static List<Option> Perfil = new List<Option>();

        public static List<Option> Status = new List<Option>();

        public static List<Option> Solicitacao = new List<Option>();

        public static List<Option> Arquivo = new List<Option>();
    }
}
