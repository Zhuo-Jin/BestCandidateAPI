using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public interface ICandidate
    {
        int candidateId { get; set; }
        string name { get; set; }
        string skillTags { get; set; }
        int score { get; set; }

        [JsonIgnore]
        Dictionary<string, int> skillSet { get; set; }
        List<string> matched { get; set; }
}
}
