using Docusign.API.Services;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docusign.API.Controllers
{
    [Route("api/templates")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {

        private readonly TemplatesApi _templatesApi;
        private readonly DocusignAccountService _docusignAccountService;

        public TemplatesController(TemplatesApi templatesApi, 
            DocusignAccountService docusignAccountService)
        {
            _templatesApi = templatesApi;
            _docusignAccountService = docusignAccountService;
        }

        [HttpGet]
        public IEnumerable<EnvelopeTemplate> ListAllTemplates()
        {

            var account = _docusignAccountService.GetAccountInfo();
            var accountId = account.AccountId;

            //TemplatesApi.ListTemplatesOptions options = new TemplatesApi.ListTemplatesOptions();
            // e.g. options.searchText = "Example Signer and CC template";
            EnvelopeTemplateResults results = _templatesApi.ListTemplates(accountId);
            var templates = results.EnvelopeTemplates;

            return templates ?? new List<EnvelopeTemplate>();
        }

    }
}
