using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAzureDemo
{
    public class SQLAzureProvider
    {
        public void AddCandidate(Candidate candidate)
        {
            var dbcontext = new SQLAzureDemo.AramPerformanceTestDBEntities();
            dbcontext.Candidates.Add(candidate);
            dbcontext.SaveChanges();
        }

        public long GetLastCandidateId()
        {
            var dbcontext = new SQLAzureDemo.AramPerformanceTestDBEntities();
            return dbcontext.Candidates.Max(m=>m.CandidateId);
        }

        public List<Candidate> GetCandidateById(int candidateId)
        {
            var dbcontext = new SQLAzureDemo.AramPerformanceTestDBEntities();
            return dbcontext.Candidates.Where(m=>m.CandidateId == candidateId).ToList();
        }

        public List<Candidate> GetCandidatesByName(string candidateName)
        {
            var dbcontext = new SQLAzureDemo.AramPerformanceTestDBEntities();
            return dbcontext.Candidates.Where(m => m.FirstName == candidateName).Take(50).ToList();
        }

        public List<Candidate> GetCandidatesList(int pageSize)
        {
            var dbcontext = new SQLAzureDemo.AramPerformanceTestDBEntities();
            return dbcontext.Candidates.Take(pageSize).ToList();
        }
    }
}
