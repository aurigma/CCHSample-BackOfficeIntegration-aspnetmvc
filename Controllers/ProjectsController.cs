using Aurigma.StorefrontApi;
using CustomersCanvasSample.Models;
using CustomersCanvasSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsApiClient _projectsApiClient;
        private readonly CustomersCanvasOptions _options;

        public ProjectsController(
            IProjectsApiClient projectsApiClient,
            IOptions<CustomersCanvasOptions> options)
        {
            _projectsApiClient = projectsApiClient;
            _options = options.Value;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProjectModel model)
        {
            CreateProjectDto project = new CreateProjectDto()
            {
                OwnerId = model.OwnerId,
                OrderId = model.OrderId,
                OrderNumber = EcommerceDataService.OrderIdToNumber(model.OrderId),
                CustomerId = model.UserId,
                CustomerName = EcommerceDataService.CustomerIdToName(model.UserId),
                ProductReference = model.ProductId,
                Items = model.Items.Select(x => new ProjectItemParametersDto()
                {
                    DesignIds = x.StateId.ToList(),
                    Fields = x.Fields,
                    Hidden = x.Hidden,
                }).ToList()
            };
            
            var result = await _projectsApiClient.CreateAsync(_options.StorefrontId, body: project);
            return new OkObjectResult(result);
        }
    }
}
