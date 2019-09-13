using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public class Job: IJob
    {
        public int jobId { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string skills { get; set; }

    }
}
