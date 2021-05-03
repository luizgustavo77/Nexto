using Commom.Proxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commom.Dto.SelectList
{
    public static class Select
    {
           
        public static List<Option> Sexo = new List<Option>()
        {
            new Option(1, "Masculino"),
            new Option(2, "Feminino"),
            new Option(3, "Outros")
        };

        public static List<Option> Perfil = new List<Option>();

        public static List<Option> Status = new List<Option>()
        {
            new Option(1, "Em Andamento"),
            new Option(2, "Finalizado")
        };

        public static List<Option> Solicitacao = new List<Option>()
        {
            new Option(1, "Patente de Invenção"),
            new Option(2, "Modelo de Utilidade")
        };

        public static List<Option> Arquivo = new List<Option>()
        {
            new Option(1, "Desenho"),
            new Option(2, "Foto")
        };
    }
}
