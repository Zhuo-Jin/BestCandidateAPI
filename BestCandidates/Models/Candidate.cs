using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public class Candidate: ICandidate
    {
        public int candidateId { get; set; }
        public string name { get; set; }
        public string skillTags { get; set; }
        public int score { get; set; }

        [JsonIgnore]
        public Dictionary<string, int> skillSet { get; set; }

        public List<string> matched { get; set; }
    }
}
