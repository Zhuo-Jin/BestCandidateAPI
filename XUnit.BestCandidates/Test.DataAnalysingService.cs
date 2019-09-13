using System;
using Xunit;
using BestCandidates.Services;
using System.Collections.Generic;
using BestCandidates.Models;
using Newtonsoft.Json;

namespace XUnit.BestCandidates
{
    public class xUnitDataAnalysingService
    {
        public static IEnumerable<object[]> RelevantSkillData =>
          new List<object[]>
          {
                new object[] { "mobile, mobile,swift, xcode ", new Dictionary<string, int> { { "mobile" , 3}, { "swift", 2 }, { "xcode", 1 } }  },
          };

        public static IEnumerable<object[]> SkillsetData =>
          new List<object[]>
          {
                new object[] { "mobile,swift, xcode ", new Dictionary<string, int> { { "mobile" , 1}, { "swift", 2 }, { "xcode", 3 } }  },
          };

        public static IEnumerable<object[]> CampareCandidateData =>
         new List<object[]>
         {
                new object[] {

                   JsonConvert.DeserializeObject<Candidate>("{'candidateId': 191,'name': 'Darline Hardy','skillTags': 'detail, administration, detail, fastlane, administration','score': 5,'skillSet': {'detail': 1,'administration': 2,'fastlane': 3}}"),
                   JsonConvert.DeserializeObject<Candidate>("{'candidateId': 18,'name': 'Karisa Glandon','skillTags': 'administration, prescriptions, service, ordering, mobile','score': 6,'skillSet': {	'administration': 1,'prescriptions': 2,	'service': 3,'ordering': 4,	'mobile': 5}}"),
                   new JobRelevantSkills(){ JobId = 1, NormalizedSkillSet = new Dictionary<string, int>() { { "administration", 5 }, { "outlook", 4 }, { "spreadsheets", 3 }, { "housekeeping", 2 }, { "ordering", 1 } } },


                },
         };


        [Theory]
        [MemberData(nameof(RelevantSkillData))]

        public void Test_NormalizedSkillset_Job(string skillstr, Dictionary<string, int> skills )
        {
            var dataAService = new DataAnalysingService();
            var normalisedSkills = dataAService.NormalizedSkillset(skillstr);

            Assert.Equal(skills, normalisedSkills);
        }

        [Theory]
        [MemberData(nameof(SkillsetData))]

        public void Test_NormalizedSkillset_Candidate(string skillstr, Dictionary<string, int> skills)
        {
            var dataAService = new DataAnalysingService();
            var normalisedSkills = dataAService.NormalizedSkillset(skillstr, false);

            Assert.Equal(skills, normalisedSkills);


        }

        [Theory]
        [MemberData(nameof(CampareCandidateData))]
        
        public void Test_OneBetterThanNext_Candidate(ICandidate current, ICandidate next, JobRelevantSkills job)
        {
            var dataAService = new DataAnalysingService();
            var result = dataAService.CurrentCandidateIsBetterThanNextOne(current, next, job);
            Assert.False(result);


        }

    }
}
