using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public class EndpointConfig : IEndpointConfig
    {
        public string Jobs { get; set; }
        public string Candicates { get; set; }
    }
}
