using System;
using System.Collections.Generic;
using System.Text;

namespace Commom.Dto.SelectList
{
    public class Option
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public Option(int id, string name)
        {
            Id = id;
            Descricao = name;
        }

        public Option() { }
    }
}
