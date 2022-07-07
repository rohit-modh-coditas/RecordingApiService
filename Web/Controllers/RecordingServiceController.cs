
using Application.Common.Models;
using Application.Recordings.Queries.GetRecordings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecordingServiceController : ApiControllerBase
    {
        // POST api/<RecordingController>
        
        [HttpPost(Name ="ProcessRecordings")]
        public async Task<Result> Post([FromQuery] GetRecordingsQuery query)//, [FromHeader] string AuthKey
        {
           return await Mediator.Send(new GetRecordingsQuery { LeadTransitId = query.LeadTransitId, context = this.HttpContext});
        }
        
        [Route("GetAllRecordings")]
        [HttpGet]
        public async Task<ActionResult<RecordingListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetRecordingListQuery { context = this.HttpContext }));
        }
    }
}
