/* Zhuo Jin 2019-09-13
    Core Logic to find the best matched candidate
    based on 2 rules

    rule #1 : candidate has more skills always better no matter some skill are strong others are weak.
    rule #2 : candidate with same skill then candidate has strongest skill also it is the most relavant to the job , then he/she is better
    
    Algorithm: 
        * each relevent skill from each job has number, bigger number represent the most relevant skill
        * each candidate has a score based on the sum of the relevent job skill(number) which also match with his/her own skill.
        * the higher score candidate is better
        * candidate's skill also mapped with number , 1 is strongest n is weakest
        * if score is the same between each candidate, 
                loop through from the most relevant job skill to less relevant skill and match with each candidate's strongest skill     
        * whoever's most strongest skill mactch most relevant job skill will be consider as better candidate 

 */

using BestCandidates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestCandidates.Extension;

namespace BestCandidates.Services
{
    public class DataAnalysingService : IDataAnalysingService
    {
        public IJob job { get; set; }

        public List<Candidate> candidates { get; set; }

        public DataAnalysingService()
        {
            
        }

        public async Task<List<ICandidate>> GetBestCandidateFromJobId(int jobId)
        {

            var jobskills = GetJobNormalizedSkillset(job);

            var candidateWithSkillmatch = new List<ICandidate>();

            foreach (var candidate in candidates) { 
                // find each relevant canidicate
                candidate.skillSet = NormalizedSkillset(candidate.skillTags, false);
                var matchedSkills = jobskills.NormalizedSkillSet.Where(ns => candidate.skillSet.Keys.Contains(ns.Key));

                candidate.score = matchedSkills.Sum(s => s.Value); // USE TO SORT THE CANDIDATE
                if (candidate.score != 0)
                {
                    candidate.matched = matchedSkills.OrderByDescending(ms => ms.Value).Select(ms => ms.Key).ToList();
                    candidateWithSkillmatch.Add(candidate);
                }
                    

            }

            return await Task.Run(() => DeepSortCandidates(candidateWithSkillmatch.OrderBy(cs => cs.score).ToList(), jobskills)) ;
        }

        public List<ICandidate> DeepSortCandidates(List<ICandidate> deepSortedCandidates, IJobRelevantSkills jobRequiredSkill)
        {
            //var deepSortedCandidates = candidateWithSkillmatch.OrderBy(cs => cs.score).ToList();
            var swapped = false;
            // start from most irrelevent candidate, if next is not as good as current, swap
            for (var i = 0; i < deepSortedCandidates.Count(); i++)
            {
                if (i < deepSortedCandidates.Count() - 1 
                    && deepSortedCandidates[i].score == deepSortedCandidates[i + 1].score
                    && CurrentCandidateIsBetterThanNextOne(deepSortedCandidates[i], deepSortedCandidates[i + 1], jobRequiredSkill))

                {
                    deepSortedCandidates.Swap(i, i + 1);
                    swapped = true;
                }

            }

            if (swapped)
            {
                deepSortedCandidates = DeepSortCandidates(deepSortedCandidates, jobRequiredSkill);
            }


            return deepSortedCandidates;
        }

        public bool CurrentCandidateIsBetterThanNextOne(ICandidate current, ICandidate next, IJobRelevantSkills jobRequiredSkill) {
            foreach (var skill in jobRequiredSkill.NormalizedSkillSet.OrderByDescending(s => s.Value).Select(s => s.Key).ToList()) {
                if (current.skillSet.ContainsKey(skill) && next.skillSet.ContainsKey(skill))
                    // 1 is strongest n is weakest, current one may have stronger skil of what job required
                    if (current.skillSet[skill] < next.skillSet[skill])
                        return true;
                    else 
                        return false;

                else if (current.skillSet.ContainsKey(skill) && !next.skillSet.ContainsKey(skill))
                    return true; // current one is better than next one , ready to re-order
                else if (!current.skillSet.ContainsKey(skill) && next.skillSet.ContainsKey(skill))
                    return false; // next one is better . no need to re-order
            }

            return false;


        }

        public IJobRelevantSkills GetJobNormalizedSkillset(IJob job)
        {
            var jobSkills = new JobRelevantSkills();

            jobSkills.JobId = job.jobId;
            jobSkills.NormalizedSkillSet = NormalizedSkillset(job.skills);

            return jobSkills;
        }

        public Dictionary<string, int> NormalizedSkillset(string skills, bool isReverse = true)
        {
            var skillPosition = 0;
            var skillinHash = new Dictionary<string, int>();

            if (isReverse)
            {
                skills.Split(',').Select(s => s.Trim()).Distinct().Reverse().ToList()
                     .ForEach(js => { skillPosition++; skillinHash.Add(js, skillPosition); });

            }
            else {
                skills.Split(',').Select(s => s.Trim()).Distinct().ToList()
                     .ForEach(js => { skillPosition++; skillinHash.Add(js, skillPosition); });
            }

            return skillinHash;
        }
    }

}
