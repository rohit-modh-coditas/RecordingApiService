
using Application.Products.Commands.CreateProductCommand;
using Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ProductVm>> Get()
        {
            return await Mediator.Send(new GetProductsQuery());
        }

       
        [HttpPost]
        [Route("Add")]
        
        public async Task<ActionResult<int>> Create([FromBody]CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }


    }
}
