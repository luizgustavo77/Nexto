using Commom.Dto.SelectList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Commom.Proxy
{
    public class APISelect
    {
        internal string _baseEndpoint;
        public string _BaseUrl { get; set; }
        public bool _ambienteTeste = false;
        internal HttpClient Http;

        public APISelect(bool ambienteTeste)
        {
            _ambienteTeste = ambienteTeste;
            Http = new HttpClient();
            _BaseUrl = "http://nextoapiapp.azurewebsites.net/";
            _baseEndpoint = "api/";
        }

        public async Task Iniciar()
        {
            Select.Perfil = await GetPerfil();
            Select.Sexo = await GetSexo();
            Select.Status = await GetStatus();
            Select.Solicitacao = await GetSolicitacao();
            Select.Arquivo = await GetArquivo();
        }

        public async Task<List<Option>> GetPerfil()
        {
            List<Option> result = new List<Option>();
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _BaseUrl + _baseEndpoint + "perfil/"))
                    {
                        var requestReturn = await Http.SendAsync(request);
                        string str = await requestReturn.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<Option>>(str);
                    }
                }
                else
                {
                    result = new List<Option>()
                    {
                        new Option(1, "Cliente"),
                        new Option(2, "Colaborador")
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Option>> GetSexo()
        {
            List<Option> result = new List<Option>();
            try
            {
                if (false/*!_ambienteTeste*/)
                {
                    result = await Http.GetFromJsonAsync<List<Option>>(_BaseUrl + _baseEndpoint + "sexo/");
                }
                else
                {
                    result = new List<Option>()
                    {
                        new Option(1, "Masculino"),
                        new Option(2, "Feminino"),
                        new Option(3, "Outros")
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Option>> GetStatus()
        {
            List<Option> result = new List<Option>();
            try
            {
                if (false/*!_ambienteTeste*/)
                {
                    result = await Http.GetFromJsonAsync<List<Option>>(_BaseUrl + _baseEndpoint + "status/");
                }
                else
                {
                    result = new List<Option>()
                    {
                        new Option(1, "Em Andamento"),
                        new Option(2, "Finalizado")
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Option>> GetSolicitacao()
        {
            List<Option> result = new List<Option>();
            try
            {
                if (false/*!_ambienteTeste*/)
                {
                    result = await Http.GetFromJsonAsync<List<Option>>(_BaseUrl + _baseEndpoint + "solicitacao/");
                }
                else
                {
                    result = new List<Option>()
                    {
                        new Option(1, "Patente de Invenção"),
                        new Option(2, "Modelo de Utilidade")
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Option>> GetArquivo()
        {
            List<Option> result = new List<Option>();
            try
            {
                if (false/*!_ambienteTeste*/)
                {
                    result = await Http.GetFromJsonAsync<List<Option>>(_BaseUrl + _baseEndpoint + "arquivo/");
                }
                else
                {
                    result = new List<Option>()
                    {
                        new Option(1, "Desenho"),
                        new Option(2, "Foto")
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
