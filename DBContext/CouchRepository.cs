using Creacion.Models;
using creation_ms.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace creation_ms.DBContext
{
    public class CouchRepository : ICouchRepository
    {
        private readonly string _couchDbUrl;
        private readonly string _couchDbName;
        private readonly string _couchDbUser;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        public CouchRepository(IConfiguration configuration, IHttpClientFactory clientFactory)
        {

            _configuration = configuration;
            _clientFactory = clientFactory;
            _couchDbUrl = this._configuration["CouchDB:URL"];
            _couchDbName = this._configuration["CouchDB:DbName"];
            _couchDbUser = this._configuration["CouchDB:User"];
        }
        /*
        public async Task<HttpClientResponse> DeleteDocumentAsync(string id, string rev)
        {
            HttpClientResponse response = new HttpClientResponse();
            var dbClient = DbHttpClient();

            //CouchDB URL : DELETE http://{hostname_or_IP}:{Port}/{couchDbName}/{_id}/?rev={_rev}  
            var dbResult = await dbClient.DeleteAsync(_couchDbName + "/" + id + "?rev=" + rev);

            if (dbResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync();
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = dbResult.ReasonPhrase;
            }
            return response;
        }*/
        
        public async Task<HttpClientResponse> GetDocumentAsync(string id, int op)
        {
            HttpClientResponse response = new HttpClientResponse();
            var dbClient = DbHttpClient();

            if (op == 1)
            {
                //CouchDB URL : GET http://{hostname_or_IP}:{Port}/{couchDbName}/{_id}  
                String aux = @"/_design/Meets/_view/id?key=""" + id + @"""";
                Console.Write(aux);
                var dbResult = await dbClient.GetAsync(_couchDbName + aux);
                //Trace.WriteLine(dbResult.Content.ReadAsStringAsync());
                if (dbResult.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    Console.WriteLine(dbResult.Content.ReadAsStringAsync());
                    response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync();
                }
                else
                {
                    response.IsSuccess = false;
                    response.FailedReason = dbResult.ReasonPhrase;
                }
                return response;
            }
            else
            {
                //CouchDB URL : GET http://{hostname_or_IP}:{Port}/{couchDbName}/{_id}  
                var dbResult = await dbClient.GetAsync(_couchDbName + "/" + id);

                if (dbResult.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    response.SuccessContentObject = await dbResult.Content.ReadAsStringAsync();
                }
                else
                {
                    response.IsSuccess = false;
                    response.FailedReason = dbResult.ReasonPhrase;
                }
                return response;
            }
            
        }


        public async Task<HttpClientResponse> PostDocumentAsync(CreationModel creationModel)
        {
            HttpClientResponse response = new HttpClientResponse();
            var dbClient = DbHttpClient();
            var jsonData = JsonConvert.SerializeObject(creationModel);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //CouchDB URL : POST http://{hostname_or_IP}:{Port}/{couchDbName}  
            var postResult = await dbClient.PostAsync(_couchDbName, httpContent).ConfigureAwait(true);

            if (postResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await postResult.Content.ReadAsStringAsync();
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = postResult.ReasonPhrase;
            }
            return response;
        }
        /*
        public async Task<HttpClientResponse> PutDocumentAsync(UpdateEnroll update)
        {
            HttpClientResponse response = new HttpClientResponse();
            var dbClient = DbHttpClient();
            var updateToDb = new
            {
                update.Name,
                update.EmailAddress,
                update.CourseName,
                update.EnrolledOn,
                update.UpdatedOn
            };
            var jsonData = JsonConvert.SerializeObject(updateToDb);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //CouchDB URL : PUT http://{hostname_or_IP}:{Port}/{couchDbName}/{_id}/?rev={_rev}  
            var putResult = await dbClient.PutAsync(_couchDbName + "/" +
                                                      update.Id +
                                                      "?rev=" + update.Rev, httpContent).ConfigureAwait(true);

            if (putResult.IsSuccessStatusCode)
            {
                response.IsSuccess = true;
                response.SuccessContentObject = await putResult.Content.ReadAsStringAsync();
            }
            else
            {
                response.IsSuccess = false;
                response.FailedReason = putResult.ReasonPhrase;
            }
            return response;
        }*/

        private HttpClient DbHttpClient()
        {
            var httpClient = this._clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Clear();

            httpClient.BaseAddress = new Uri(_couchDbUrl);
            var dbUserByteArray = Encoding.ASCII.GetBytes(_couchDbUser);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(dbUserByteArray));
            return httpClient;
        }
    }
}
