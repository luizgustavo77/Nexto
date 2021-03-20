using Commom.Dto;
using Commom.Dto.Core;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APIUser : APIBase<UserDto>
    {
        public APIUser(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "https://localhost:44347";
            _baseEndpoint = "API/User";
        }

        public async Task<UserDto> Login(UserDto Item)
        {
            UserDto user = new UserDto();
            try
            {
                if (!_ambienteTeste)
                    user = await Http.GetFromJsonAsync<UserDto>(_BaseUrl + "/" + _baseEndpoint + "/Login/" + Item.Login + "/" + Item.PassWord);
                else if (Item != null && Item.Login.Equals("Admin") && Item.PassWord.Equals("nota10"))
                {
                    Item.Perfil = "Colaborador";
                    Item.Id = 1;
                    Item.Name = "Administrador Teste";
                    user = Item;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Task<UserDto> Find(int Id)
        {
            if (!_ambienteTeste)
                return base.Find(Id);
            else
            {
                UserDto user = new UserDto()
                {
                    BirthDate = DateTime.Now,
                    Cidade = "Santo André",
                    Estado = "São Paulo",
                    CPF = "123.123.123.-23",
                    Email = "email@hotmail.com",
                    Id = 1,
                    Login = "Teste",
                    Name = "Teste",
                    PassWord = "Senha",
                    Perfil = "Colaborador",
                    Sexo = "Outros",
                    Telefone = "(11) 98888-8888"
                };

                return Task.Delay(100).ContinueWith(t => user);
            }
        }

        public override Task<List<UserDto>> GetAll()
        {
            if (!_ambienteTeste)
                return base.GetAll();
            else
            {

                UserDto user1 = new UserDto()
                {
                    BirthDate = DateTime.Now,
                    Cidade = "Santo André1",
                    Estado = "São Paulo1",
                    CPF = "123.123.123.-21",
                    Email = "email@hotmail.com1",
                    Id = 1,
                    Login = "Teste1",
                    Name = "Teste1",
                    PassWord = "Senha1",
                    Perfil = "Colaborador1",
                    Sexo = "Outros1",
                    Telefone = "(11) 98888-8881"
                };

                UserDto user2 = new UserDto()
                {
                    BirthDate = DateTime.Now,
                    Cidade = "Santo André2",
                    Estado = "São Paulo2",
                    CPF = "123.123.123.-22",
                    Email = "email@hotmail.com2",
                    Id = 2,
                    Login = "Teste2",
                    Name = "Teste2",
                    PassWord = "Senha2",
                    Perfil = "Colaborador2",
                    Sexo = "Outros2",
                    Telefone = "(11) 98888-8882"
                };

                UserDto user3 = new UserDto()
                {
                    BirthDate = DateTime.Now,
                    Cidade = "Santo André3",
                    Estado = "São Paulo3",
                    CPF = "123.123.123.-23",
                    Email = "email@hotmail.com3",
                    Id = 3,
                    Login = "Teste3",
                    Name = "Teste3",
                    PassWord = "Senha3",
                    Perfil = "Colaborador3",
                    Sexo = "Outros3",
                    Telefone = "(11) 98888-8883"
                };

                List<UserDto> users = new List<UserDto>() { user1, user2, user3 };

                return Task.Delay(100).ContinueWith(t => users);
            }
        }
    }
}
