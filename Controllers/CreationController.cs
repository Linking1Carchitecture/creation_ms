using Creacion.Models;
using creation_ms.DBContext;
using creation_ms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace creation_ms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreationController : ControllerBase
    {
        private readonly ILogger<CreationController> _logger;
        private readonly ICouchRepository _couchRepository;
        public CreationController(ILogger<CreationController> logger, ICouchRepository couchRepository)
        {
            _logger = logger;
            _couchRepository = couchRepository;
        }

        [HttpPost]
        [Route("normal")]
        public async Task<IActionResult> PostAsync([FromBody] CreationModel creationModel)
        {
            var result = await _couchRepository.PostDocumentAsync(creationModel);
            if (result.IsSuccess)
            {
                var sResult = JsonConvert.DeserializeObject<SavedResult>(result.SuccessContentObject);
                Trace.WriteLine(creationModel.Id_llam);
                //return new CreatedResult("Success", sResult);
                var sResult2 = JsonConvert.DeserializeObject<CreationModel>(Newtonsoft.Json.JsonConvert.SerializeObject(creationModel));
                return new CreatedResult("Success", sResult2);

                //return new CreatedResult("Success",Newtonsoft.Json.JsonConvert.SerializeObject(creationModel));
            }

            return new UnprocessableEntityObjectResult(result.FailedReason);
        }

        [HttpPost]
        [Route("fecha")]
        public async Task<IActionResult> Fecha([FromBody] CreationModel creationModel)
        {
            DateTime today = DateTime.Today;
            creationModel.Begin_Date = today.ToString();
            var result = await _couchRepository.PostDocumentAsync(creationModel);
            if (result.IsSuccess)
            {
                var sResult = JsonConvert.DeserializeObject<SavedResult>(result.SuccessContentObject);
                return new CreatedResult("Success", sResult);

            }

            return new UnprocessableEntityObjectResult(result.FailedReason);
        }

    }
}
