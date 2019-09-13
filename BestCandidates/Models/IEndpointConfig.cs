using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public interface IEndpointConfig
    {
        string Jobs { get; set; }
        string Candicates { get; set; }
    }
}
