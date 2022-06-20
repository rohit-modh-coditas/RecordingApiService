using Application.ApplicationUser.Queries.GetToken;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        // <summary>
        /// Login and get JWT token email: test@test.com password: Matech_1850
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorizationResult>> Login([FromBody]UserAuthQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
