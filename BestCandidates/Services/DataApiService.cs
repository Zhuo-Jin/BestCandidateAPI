using BestCandidates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BestCandidates.Services
{
    public class DataApiService : IDataApiService
    {
        private readonly string _jobUrl  ;
        private readonly string _candidateUrl;
        public DataApiService(IConfiguration configuration) {

            _jobUrl = configuration.GetSection("Endpoints").Get<EndpointConfig>().Jobs;
            _candidateUrl = configuration.GetSection("Endpoints").Get<EndpointConfig>().Candicates;

        }

        public async Task<List<Job>> GetJobs() {
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    //Post http callas.
                    HttpResponseMessage response = client.GetAsync(_jobUrl).Result;

                    response.EnsureSuccessStatusCode();
                    //response to string

                    var jobList = await Task.Run(() => JsonConvert.DeserializeObject<List<Job>>(response.Content.ReadAsStringAsync().Result));
                    return jobList;

                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
        public async Task<IJob> GetJobByIdAysnc(int Id)
        {
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    //Post http callas.
                    HttpResponseMessage response = client.GetAsync(_jobUrl).Result;

                    response.EnsureSuccessStatusCode();
                    //response to string

                    var jobList = await Task.Run(() => JsonConvert.DeserializeObject<List<Job>>(response.Content.ReadAsStringAsync().Result));
                    return jobList.Find(l => l.jobId == Id);

                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }




        public async Task<List<Candidate>> GetCandidatesAysnc()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Post http callas.
                    HttpResponseMessage response = client.GetAsync(_candidateUrl).Result;

                    response.EnsureSuccessStatusCode();
                    //response to string
                    var candidates = await Task.Run(() => JsonConvert.DeserializeObject<List<Candidate>>(response.Content.ReadAsStringAsync().Result));

                    return candidates;

                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
      
    }
}
