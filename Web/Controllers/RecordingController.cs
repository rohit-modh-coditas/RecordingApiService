
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
    public class RecordingController : ApiControllerBase
    {
        // GET: api/<RecordingController>
        [HttpGet]
        public async Task<ConversationRecordingViewModel> fetchRecordingAsync([FromQuery] GetRecordingsQuery query)
        {

            return await Mediator.Send(query);
        }

        //[HttpGet]
        //public async Task<ActionResult<RecordingListVm>> Get()
        //{
        //    return await Mediator.Send(new GetRecordingListQuery());
        //}
        
        [Route("GetAllRecordings")]
        [HttpGet]
        public async Task<ActionResult<RecordingListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetRecordingListQuery { context = this.HttpContext }));
        }

        //[HttpGet]
        //public async Task<IEnumerable<ConversationRecordingViewModel>> RecordingList()
        //{

        //    return await Mediator.Send(new GetRecordingListQuery());
        //}

        // GET api/<RecordingController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<RecordingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RecordingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecordingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
