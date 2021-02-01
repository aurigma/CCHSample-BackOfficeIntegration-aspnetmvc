using Microsoft.AspNetCore.Mvc;
using Aurigma.BackOffice;
using System;
using System.Collections.Generic;
using CustomersCanvasSample.Services;
using System.Linq;
using System.Threading.Tasks;
using CustomersCanvasSample.Models;

namespace CustomersCanvasSample.Controllers
{
    public class ProjectsController : Controller
    {
        private IProjectsApiClient projectsApiClient;
        public ProjectsController(IProjectsApiClient projectsApiClient)
        {
            this.projectsApiClient = projectsApiClient;
        }

        // POST: ProjectsController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProjectDetailsModel model)
        {
            CreateProjectDto project = new CreateProjectDto()
            {
                OrderId = model.OrderId,
                OrderNumber = EcommerceDataService.OrderIdToNumber(model.OrderId),
                CustomerId = model.UserId,
                CustomerName = EcommerceDataService.CustomerIdToName(model.UserId),
                EcommerceProductId = model.ProductId,
                Products = model.LineItems.ToList()
            };
            var result = await projectsApiClient.CreateAsync(body: project);
            return new OkObjectResult(result);
        }

    }
}
