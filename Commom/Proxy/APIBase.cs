using Commom.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Commom.Proxy
{
    public class APIBase<TInterface> where TInterface : BaseDto
    {
        internal string _baseEndpoint;
        public string _BaseUrl { get; set; }
        public bool _ambienteTeste = false;
        internal HttpClient Http;

        public RetornaAcaoDto ReturnTeste()
        {
            return new RetornaAcaoDto()
            {
                Retorno = false,
                Mensagem = "Ambiente de teste"
            };
        }

        internal APIBase()
        {
            Http = new HttpClient();
        }

        public virtual async Task<RetornaAcaoDto> Add(TInterface Item)
        {
            RetornaAcaoDto retorna = new RetornaAcaoDto();
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _BaseUrl + _baseEndpoint))
                    {
                        string contentToSend = JsonConvert.SerializeObject(Item);
                        request.Content = new StringContent(contentToSend, Encoding.UTF8, "application/json");
                        var requestReturn = await Http.SendAsync(request);

                        if (requestReturn.IsSuccessStatusCode)
                        {
                            retorna.Retorno = true;
                        }
                    }
                }
                else
                    retorna = ReturnTeste();

                return retorna;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<RetornaAcaoDto> Edit(TInterface Item)
        {
            RetornaAcaoDto retorna = new RetornaAcaoDto();
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, _BaseUrl + _baseEndpoint))
                    {
                        string contentToSend = JsonConvert.SerializeObject(Item);
                        request.Content = new StringContent(contentToSend, Encoding.UTF8, "application/json");
                        var requestReturn = await Http.SendAsync(request);

                        if (requestReturn.IsSuccessStatusCode)
                        {
                            retorna.Retorno = true;
                        }
                    }
                }
                else
                    retorna =ReturnTeste();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorna;
        }


        public virtual async Task<RetornaAcaoDto> Delete(int Id)
        {
            RetornaAcaoDto retorna = new RetornaAcaoDto();
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, _BaseUrl + _baseEndpoint))
                    {
                        string contentToSend = JsonConvert.SerializeObject(Id);
                        request.Content = new StringContent(contentToSend, Encoding.UTF8, "application/json");
                        var requestReturn = await Http.SendAsync(request);

                        if (requestReturn.IsSuccessStatusCode)
                        {
                            retorna.Retorno = true;
                        }
                    }
                }
                else
                    retorna = ReturnTeste();
            }
            catch (Exception)
            {

            }
            return retorna;
        }


        public virtual async Task<TInterface> Find(int Id)
        {
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _BaseUrl + _baseEndpoint + Id.ToString()))
                    {
                        var requestReturn = await Http.SendAsync(request);
                        string str =
                            await requestReturn.Content.ReadAsStringAsync();
                        str = str.Replace("[", "").Replace("]", "");
                        return JsonConvert.DeserializeObject<TInterface>(str);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public virtual async Task<List<TInterface>> GetAll()
        {
            try
            {
                if (!_ambienteTeste)
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _BaseUrl + _baseEndpoint))
                    {
                        var requestReturn = await Http.SendAsync(request);
                        string str =
                            await requestReturn.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<TInterface>>(str);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }


        public virtual async Task<List<TInterface>> ByParentId(int Id)
        {
            var list = await Http.GetFromJsonAsync<List<TInterface>>(_BaseUrl + _baseEndpoint + "ByParentId/" + Id.ToString());
            return list;
        }
    }
}
