using Creacion.Models;
using creation_ms.DBContext;
using creation_ms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> PostAsync([FromBody] CreationModel creationModel)
        {
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
