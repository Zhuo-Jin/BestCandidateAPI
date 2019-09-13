using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Models
{
    public interface IJobRelevantSkills
    {

        int JobId { get; set; }
        Dictionary<string, int> NormalizedSkillSet { get; set; }
    }
}
