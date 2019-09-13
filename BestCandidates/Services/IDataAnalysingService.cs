using BestCandidates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCandidates.Services
{
    public interface IDataAnalysingService
    {
        IJob job { get; set; }

        List<Candidate> candidates { get; set; }
        Task<List<ICandidate>> GetBestCandidateFromJobId(int jobId);
        List<ICandidate> DeepSortCandidates(List<ICandidate> deepSortedCandidates, IJobRelevantSkills jobRequiredSkill);

        bool CurrentCandidateIsBetterThanNextOne(ICandidate current, ICandidate next, IJobRelevantSkills jobRequiredSkill);

        IJobRelevantSkills GetJobNormalizedSkillset(IJob job);

        Dictionary<string, int> NormalizedSkillset(string skills, bool isReverse);
    }
}
