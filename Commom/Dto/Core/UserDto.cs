using Commom.Dto.SelectList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Commom.Dto.Core
{
    public class UserDto : BaseDto
    {
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Confirmar a senha")]
        public string SenhaConfirm { get; set; }
        //--
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Data de nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Formato da data invalido!")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Sexo")]
        public int Sexo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        //--
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Perfil { get; set; }
    }
}
