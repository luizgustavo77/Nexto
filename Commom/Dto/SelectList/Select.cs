using System;
using System.Collections.Generic;
using System.Text;

namespace Commom.Dto.SelectList
{
    public class Select
    {
        public List<Option> Sexo = new List<Option>()
        {
            new Option(1, "Masculino"),
            new Option(2, "Feminino"),
            new Option(3, "Outros")
        };

        public List<Option> Perfil = new List<Option>()
        {
            new Option(1, "Cliente"),
            new Option(2, "Colaborador")
        };

        public List<Option> Status = new List<Option>()
        {
            new Option(1, "Em Andamento"),
            new Option(2, "Finalizado")
        };

        public List<Option> Solicitacao = new List<Option>()
        {
            new Option(1, "Patente de Invenção"),
            new Option(2, "Modelo de Utilidade")
        };

        public List<Option> Arquivo = new List<Option>()
        {
            new Option(1, "Desenho"),
            new Option(2, "Foto")
        };
    }
}
