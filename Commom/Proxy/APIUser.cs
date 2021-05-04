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
            _BaseUrl = "http://nextoapiapp.azurewebsites.net/";
            _baseEndpoint = "api/usuario";
        }

        public async Task<UserDto> Login(UserDto item)
        {
            UserDto user = new UserDto();
            try
            {
                if (!_ambienteTeste)
                {
                    var result = await Http.PostAsJsonAsync<UserDto>(_BaseUrl + _baseEndpoint + "/autenticar", item);
                    var list = await result.Content.ReadFromJsonAsync<List<UserDto>>();
                }
                else if (item != null && item.Usuario.Equals("Admin") && item.Senha.Equals("nota10"))
                {
                    item.Perfil = 2;
                    item.Id = 1;
                    item.Nome = "Administrador Teste";
                    user = item;
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
                    Cpf = "123.123.123.-23",
                    Email = "email@hotmail.com",
                    Id = 1,
                    Usuario = "Teste",
                    Nome = "Teste",
                    Senha = "Senha",
                    Perfil = 2,
                    Sexo = 3,
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
                    Cpf = "123.123.123.-21",
                    Email = "email@hotmail.com1",
                    Id = 1,
                    Usuario = "Teste1",
                    Nome = "Teste1",
                    Senha = "Senha1",
                    Perfil = 2,
                    Sexo = 3,
                    Telefone = "(11) 98888-8881"
                };

                UserDto user2 = new UserDto()
                {
                    BirthDate = DateTime.Now,
                    Cidade = "Santo André2",
                    Estado = "São Paulo2",
                    Cpf = "123.123.123.-22",
                    Email = "email@hotmail.com2",
                    Id = 2,
                    Usuario = "Teste2",
                    Nome = "Teste2",
                    Senha = "Senha2",
                    Perfil = 2,
                    Sexo = 3,
                    Telefone = "(11) 98888-8882"
                };

                UserDto user3 = new UserDto()
                {
                    BirthDate = DateTime.Now,
                    Cidade = "Santo André3",
                    Estado = "São Paulo3",
                    Cpf = "123.123.123.-23",
                    Email = "email@hotmail.com3",
                    Id = 3,
                    Usuario = "Teste3",
                    Nome = "Teste3",
                    Senha = "Senha3",
                    Perfil = 2,
                    Sexo = 3,
                    Telefone = "(11) 98888-8883"
                };

                List<UserDto> users = new List<UserDto>() { user1, user2, user3 };

                return Task.Delay(100).ContinueWith(t => users);
            }
        }
    }
}
