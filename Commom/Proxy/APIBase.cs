using Commom.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
                var result = await Http.PostAsJsonAsync<TInterface>(_BaseUrl + "/" + _baseEndpoint, Item);
                try
                {
                    if (!_ambienteTeste)
                        retorna = await result.Content.ReadFromJsonAsync<RetornaAcaoDto>();
                    else
                        retorna = await Task.Run<RetornaAcaoDto>(async () => ReturnTeste());
                }
                catch (Exception)
                {

                }

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
                var result = await Http.PutAsJsonAsync<TInterface>(_BaseUrl + "/" + _baseEndpoint, Item);

                try
                {
                    if (!_ambienteTeste)
                        retorna = await result.Content.ReadFromJsonAsync<RetornaAcaoDto>();
                    else
                        retorna = await Task.Run<RetornaAcaoDto>(async () => ReturnTeste());
                }
                catch (Exception)
                {

                }

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
            var result = await Http.DeleteAsync(_BaseUrl + "/" + _baseEndpoint + "/" + Id.ToString());
            try
            {
                if (!_ambienteTeste)
                    retorna = await result.Content.ReadFromJsonAsync<RetornaAcaoDto>();
                else
                    retorna = await Task.Run<RetornaAcaoDto>(async () => ReturnTeste());
            }
            catch (Exception)
            {

            }
            return retorna;
        }


        public virtual async Task<TInterface> Find(int Id)
        {
            var item = await Http.GetFromJsonAsync<TInterface>(_BaseUrl + "/" + _baseEndpoint + "/" + Id.ToString());
            return item;
        }

        public virtual async Task<List<TInterface>> GetAll()
        {
            var list = await Http.GetFromJsonAsync<List<TInterface>>(_BaseUrl + "/" + _baseEndpoint);
            return list;
        }


        public virtual async Task<List<TInterface>> ByParentId(int Id)
        {
            var list = await Http.GetFromJsonAsync<List<TInterface>>(_BaseUrl + "/" + _baseEndpoint + "/ByParentId/" + Id.ToString());
            return list;
        }
    }
}
