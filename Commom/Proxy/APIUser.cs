using Nexto.Commom.Dto;
using Nexto.Commom.Dto.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nexto.Commom.Proxy
{
    public class APIUser : APIBase<UserDto>
    {
        public APIUser(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "http://nextoapiapp.azurewebsites.net/";
            _baseEndpoint = "api/usuario/";
        }

        public override Task<RetornaAcaoDto> Add(UserDto Item)
        {
            _baseEndpoint += "registrar/";
            return base.Add(Item);
        }

        public async Task<UserDto> Login(UserDto item)
        {
            UserDto user = new UserDto();
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _BaseUrl + _baseEndpoint + "autenticar/"))
                    {
                        if (item != null)
                        {
                            string contentToSend = JsonConvert.SerializeObject(item);
                            request.Content = new StringContent(contentToSend, Encoding.UTF8, "application/json");
                        }
                        var result = await Http.SendAsync(request);
                        user = JsonConvert.DeserializeObject<UserDto>(await result.Content.ReadAsStringAsync());
                    }
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
