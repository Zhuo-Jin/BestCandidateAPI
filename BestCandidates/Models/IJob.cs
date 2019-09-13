using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public interface IJob
    {
        int jobId { get; set; }
        string name { get; set; }
        string company { get; set; }
        string skills { get; set; }
    }
}
