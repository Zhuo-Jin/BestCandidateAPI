using BestCandidates.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Services
{
    public interface IDataApiService
    {
        Task<IJob> GetJobByIdAysnc(int Id);
        Task<List<Candidate>> GetCandidatesAysnc();
        Task<List<Job>> GetJobs();
    }
}
