using DocuSign.eSign.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Docusign.API.Controllers
{
    [Route("api/envelopes")]
    [ApiController]
    public class EnvelopesController : ControllerBase
    {

        private readonly ILogger<EnvelopesController> _logger;
        private readonly EnvelopesApi _envelopesApi;

        public EnvelopesController(ILogger<EnvelopesController> logger,
            EnvelopesApi envelopesApi)
        {
            _logger = logger;
            _envelopesApi = envelopesApi;
        }

    }
}
