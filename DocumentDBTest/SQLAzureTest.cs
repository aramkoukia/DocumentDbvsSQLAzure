using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using DocumentDBDemo;
using System.Diagnostics;

namespace DocumentDBTest
{
    [TestClass]
    public class SQLAzureTest
    {

        [TestInitialize]
        private void Initilize()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
        }

        [TestMethod]
        public void Add10CandidatesToSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();
            for (int i = 0; i < 10; i++)
            { 
                var candidate = new SQLAzureDemo.Candidate()
                {
                    FirstName = "Aram",
                    LastName = "Koukia",
                    CandidateId = maxId + i,
                    CandidateStatusId = 1
                };
                sut.AddCandidate(candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 10 rows to SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void Add100CandidatesToSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();
            for (int i = 0; i < 100; i++)
            {
                var candidate = new SQLAzureDemo.Candidate()
                {
                    FirstName = "Aram",
                    LastName = "Koukia",
                    CandidateId = maxId + i,
                    CandidateStatusId = 1
                };
                sut.AddCandidate(candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 100 rows to SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void Add1000CandidatesToSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                var candidate = new SQLAzureDemo.Candidate()
                {
                    FirstName = "Aram",
                    LastName = "Koukia",
                    CandidateId = maxId + i,
                    CandidateStatusId = 1
                };
                sut.AddCandidate(candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 1000 rows to SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void Add10000CandidatesToSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var candidate = new SQLAzureDemo.Candidate()
                {
                    FirstName = "Aram",
                    LastName = "Koukia",
                    CandidateId = maxId + i,
                    CandidateStatusId = 1
                };
                sut.AddCandidate(candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 10000 rows to SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidateByIdFromSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();

            var result = sut.GetCandidateById(1);

            stopWatch.Stop();
            Trace.WriteLine("Read candidates by Id from SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesByNameFromSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();

            var result = sut.GetCandidatesByName("Aram");

            stopWatch.Stop();
            Trace.WriteLine("Read candidates by Name from SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesListFromSQLAzure()
        {
            Stopwatch stopWatch = new Stopwatch();

            var sut = new SQLAzureDemo.SQLAzureProvider();
            var maxId = sut.GetLastCandidateId() + 1;

            stopWatch.Start();

            var result = sut.GetCandidatesList(1000);

            stopWatch.Stop();
            Trace.WriteLine("Read 1000 candidates from SQL Azure took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

    }
}
