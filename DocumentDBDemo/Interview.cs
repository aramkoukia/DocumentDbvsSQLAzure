using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDBDemo
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public CandidateStatus Status { get; set; }
    }

    public class CandidateStatus
    {
        public int CandidateStatusId { get; set; }
        public string CandidateStatusName { get; set; }
    }
}
