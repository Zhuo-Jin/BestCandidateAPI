using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestCandidates.Models;
using BestCandidates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BestCandidates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {   
        private readonly IDataApiService _dataService;
        private readonly IDataAnalysingService _dataAnalysingService;

        public JobsController(IDataApiService dataService, IDataAnalysingService dataAnalysingService) {
            this._dataService = dataService;
            this._dataAnalysingService = dataAnalysingService;
        }

        // GET api/Jobs
        [HttpGet("GetCandidateByJobId/{id}")]
        public async Task<ActionResult<IEnumerable<ICandidate>>> GetCandidateByJobId([FromRoute]  int id)
        {
            //var candidates
            _dataAnalysingService.job = await _dataService.GetJobByIdAysnc(id);
            _dataAnalysingService.candidates = await _dataService.GetCandidatesAysnc();

            var result = await _dataAnalysingService.GetBestCandidateFromJobId(id);

            result.Reverse();

            return result;

        }

        // GET api/Jobs
        [HttpGet()]
        public async Task<ActionResult<Job>> Get()
        {
            return Ok(await _dataService.GetJobs());
        }

    }
}
