using Commom.Dto;
using Commom.Dto.Core;
using Commom.Dto.Solicitacao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APISolicitacao : APIBase<SolicitacaoDto>
    {
        public APISolicitacao(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "http://nextoapiapp.azurewebsites.net/";
            _baseEndpoint = "api/solicitacao/";
        }

        public async Task<RetornaAcaoDto> AddArquivos(List<ArquivoDto> arquivos)
        {
            RetornaAcaoDto retorna = new RetornaAcaoDto();
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _BaseUrl + _baseEndpoint))
                    {
                        string contentToSend = JsonConvert.SerializeObject(arquivos);
                        request.Content = new StringContent(contentToSend, Encoding.UTF8, "application/json");
                        var requestReturn = await Http.SendAsync(request);

                        if (requestReturn.IsSuccessStatusCode)
                        {
                            retorna.Retorno = true;
                        }
                    }
                    retorna = new RetornaAcaoDto() { Retorno = true };
                }
                else
                {
                    retorna = ReturnTeste();
                }

                return retorna;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<RetornaAcaoDto> DeleteArquivo(int Id)
        {
            RetornaAcaoDto retorna = new RetornaAcaoDto();
            var result = await Http.DeleteAsync(_BaseUrl + "/" + _baseEndpoint + Id.ToString());
            try
            {
                if (!_ambienteTeste)
                    retorna = await result.Content.ReadFromJsonAsync<RetornaAcaoDto>();
                else
                    retorna = ReturnTeste();
            }
            catch (Exception)
            {

            }
            return retorna;
        }

        public override Task<SolicitacaoDto> Find(int Id)
        {
            if (!_ambienteTeste)
                return base.Find(Id);
            else
            {
                SolicitacaoDto solicitacao = new SolicitacaoDto()
                {
                    Id = 1,
                    Cliente = new Dto.Core.UserDto()
                    {
                        Id = 1,
                        Nome = "Cliente"
                    },
                    Colaborador = new Dto.Core.UserDto()
                    {
                        Id = 2,
                        Nome = "Colaborador"
                    },
                    DataFim = DateTime.Now.AddDays(1),
                    DataInicio = DateTime.Now,
                    Nome = "Solicitacao",
                    Sigla = "SO",
                    Status = 1,
                    Tipo = 1,
                    Formularios = new List<FormularioDto>()
                    {
                        new FormularioDto()
                        {
                            Id = 1,
                            Enviado = DateTime.Now,
                            Responsavel = new UserDto()
                            {
                                Id = 1,
                                Nome= "Luiz",
                            }
                        },
                        new FormularioDto()
                        {
                            Id = 2,
                            Enviado = DateTime.Now,
                            Responsavel = new UserDto()
                            {
                                Id = 2,
                                Nome= "Paulo",
                            }
                        }
                    }
                };

                return Task.Delay(100).ContinueWith(t => solicitacao);
            }
        }

        public override Task<List<SolicitacaoDto>> GetAll()
        {
            if (!_ambienteTeste)
                return base.GetAll();
            else
            {
                SolicitacaoDto solicitacao1 = new SolicitacaoDto()
                {
                    Id = 1,
                    Cliente = new Dto.Core.UserDto()
                    {
                        Id = 1,
                        Nome = "Cliente1"
                    },
                    Colaborador = new Dto.Core.UserDto()
                    {
                        Id = 2,
                        Nome = "Colaborador1"
                    },
                    DataFim = DateTime.Now.AddDays(1),
                    DataInicio = DateTime.Now,
                    Nome = "Solicitacao1",
                    Sigla = "SO1",
                    Status = 1,
                    Tipo = 1,
                    Formularios = new List<FormularioDto>()
                    {
                        new FormularioDto()
                        {
                            Id = 1,
                            Enviado = DateTime.Now,
                            Responsavel = new UserDto()
                            {
                                Id = 1,
                                Nome= "Luiz",
                            }
                        },
                        new FormularioDto()
                        {
                            Id = 2,
                            Enviado = DateTime.Now,
                            Responsavel = new UserDto()
                            {
                                Id = 2,
                                Nome= "Paulo",
                            }
                        }
                    }
                };

                SolicitacaoDto solicitacao2 = new SolicitacaoDto()
                {
                    Id = 2,
                    Cliente = new Dto.Core.UserDto()
                    {
                        Id = 1,
                        Nome = "Cliente2"
                    },
                    Colaborador = new Dto.Core.UserDto()
                    {
                        Id = 2,
                        Nome = "Colaborador2"
                    },
                    DataFim = DateTime.Now.AddDays(1),
                    DataInicio = DateTime.Now,
                    Nome = "Solicitacao2",
                    Sigla = "SO2",
                    Status = 1,
                    Tipo = 1,
                    Formularios = new List<FormularioDto>()
                    {
                        new FormularioDto()
                        {
                            Id = 1,
                            Enviado = DateTime.Now,
                            Responsavel = new UserDto()
                            {
                                Id = 1,
                                Nome= "Luiz",
                            }
                        },
                        new FormularioDto()
                        {
                            Id = 2,
                            Enviado = DateTime.Now,
                            Responsavel = new UserDto()
                            {
                                Id = 2,
                                Nome= "Paulo",
                            }
                        }
                    }
                };

                List<SolicitacaoDto> solicitacoes = new List<SolicitacaoDto>() { solicitacao1, solicitacao2 };

                return Task.Delay(100).ContinueWith(t => solicitacoes);
            }
        }
    }
}
