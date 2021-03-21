using Commom.Dto.Core;
using Commom.Dto.Solicitacao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APISolicitacao : APIBase<SolicitacaoDto>
    {
        public APISolicitacao(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            _BaseUrl = "https://localhost:44347";
            _baseEndpoint = "API/Solicitacao";
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
                        Id =1,
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
                    Status = "Em Andamento",
                    Tipo = "Mecnica",                    
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
                    Status = "Em Andamento1",
                    Tipo = "Mecnica1",
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
                    Status = "Em Andamento2",
                    Tipo = "Mecnica2",
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

                List<SolicitacaoDto> solicitacoes = new List<SolicitacaoDto>() { solicitacao1, solicitacao2};

                return Task.Delay(100).ContinueWith(t => solicitacoes);
            }
        }
    }
}
