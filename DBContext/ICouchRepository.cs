using Creacion.Models;
using creation_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace creation_ms.DBContext
{
    public interface ICouchRepository
    {
        Task<HttpClientResponse> PostDocumentAsync(CreationModel creationModel);
        //Task<HttpClientResponse> PutDocumentAsync(UpdateEnroll update);
        Task<HttpClientResponse> GetDocumentAsync(string Id_llam);
        //Task<HttpClientResponse> DeleteDocumentAsync(string id, string rev);
    }
}
