using Microsoft.Extensions.Configuration;
using MvcPracticaConciertosAWS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcPracticaConciertosAWS.Services
{
    public class ServiceApiConciertos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiConciertos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiConciertosAWS");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApi<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string url = this.UrlApi + request;
                HttpResponseMessage response =
                await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                    await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            string request = "/api/Conciertos";
            List<Evento> eventos =
            await this.CallApi<List<Evento>>(request);
            return eventos;
        }

        public async Task<List<CategoriaEvento>> GetCategorias()
        {
            string request = "/api/Conciertos/GetCategorias";
            List<CategoriaEvento> categorias =
            await this.CallApi<List<CategoriaEvento>>(request);
            return categorias;
        }

        public async Task<List<Evento>> GetEventosCategoriaAsync(int idcategoria)
        {
            string request = "/api/Conciertos/GetEventosCategoria/"+idcategoria;
            List<Evento> eventos =
            await this.CallApi<List<Evento>>(request);
            return eventos;
        }

        public async Task<Evento> FindEvento(int idevento)
        {
            string request = "/api/Conciertos/"+idevento;
            Evento evento =
            await this.CallApi<Evento>(request);
            return evento;
        }

        public async Task InsertarEvento(Evento ev)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Conciertos/";
               
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string url = this.UrlApi + request;
                string json = JsonConvert.SerializeObject(ev);

                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(url, content);
            }
        }

        public async Task UpdateEvento(int idevento, int idcategoria)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Conciertos/"+idevento+"/"+ idcategoria;
                string relleno = "";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string url = this.UrlApi + request;
                string json = JsonConvert.SerializeObject(relleno);

                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(url, content);
            }
        }

          public async Task DeleteEventoAsync(int idevento)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "/api/Conciertos/" + idevento;
            
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string url = this.UrlApi + request;
           
                HttpResponseMessage response =
                    await client.DeleteAsync(url);
            }
        }

    }
}
