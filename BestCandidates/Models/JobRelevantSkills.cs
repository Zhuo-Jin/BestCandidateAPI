using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public class JobRelevantSkills : IJobRelevantSkills
    {
        public int JobId { get; set; }
        public Dictionary<string, int> NormalizedSkillSet { get; set; }

    }
}
