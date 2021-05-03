using Commom.Dto;
using Commom.Dto.Core;
using Commom.Dto.SelectList;
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
            if (Select.Perfil == null || Select.Perfil.Count == 0)
            {
                Select.Perfil = await GetPerfil();
            }
        }

        public async Task<List<Option>> GetPerfil()
        {
            List<Option> result = new List<Option>();
            try
            {
                if (!_ambienteTeste)
                    result = await Http.GetFromJsonAsync<List<Option>>(_BaseUrl + _baseEndpoint + "perfil/");
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

    }
}
