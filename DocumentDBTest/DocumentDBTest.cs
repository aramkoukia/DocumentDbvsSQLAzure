using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using DocumentDBDemo;
using System.Diagnostics;
using Microsoft.Azure.Documents.Client;

namespace DocumentDBTest
{
    [TestClass]
    public class DocumentDBTest
    {

        [TestInitialize]
        private void Initilize()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
            var sut = new DocumentDBDemo.DocumentDBProvider();
        }


        [TestMethod]
        public async Task CreateDatabaseAndDocumentCollection_Test()
        {
            
            var sut = new DocumentDBDemo.DocumentDBProvider();
            var result = await sut.CreateDatabaseAndDocumentCollection();
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task AddCanidateToCollection_Test()
        {

            var sut = new DocumentDBDemo.DocumentDBProvider();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();
            var candidate = new Candidate()
            {
                CandidateFirstName = "Aram",
                CandidateLastName = "Koukia",
                CandidateId = 1,
                Status = new CandidateStatus() { 
                     CandidateStatusId= 1,
                     CandidateStatusName = "New"
                }
            };
            var result = await sut.AddCanidateToCollection(documentLink, documentClient, candidate);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetCandidatesCollection_Test()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            var result = sut.GetCandidatesCollection();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCandidateById_Test()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            var result = sut.GetCandidateById(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add10CandidatesToDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            for (int i = 0; i < 10; i++)
            {
                var candidate = new Candidate()
                {
                    CandidateFirstName = "Aram",
                    CandidateLastName = "Koukia",
                    CandidateId = 1,
                    Status = new CandidateStatus()
                    {
                        CandidateStatusId = 1,
                        CandidateStatusName = "New"
                    }
                };
                await sut.AddCanidateToCollection(documentLink, documentClient, candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 10 rows to DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public async Task Add100CandidatesToDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            for (int i = 0; i < 100; i++)
            {
                var candidate = new Candidate()
                {
                    CandidateFirstName = "Aram",
                    CandidateLastName = "Koukia",
                    CandidateId = 1,
                    Status = new CandidateStatus()
                    {
                        CandidateStatusId = 1,
                        CandidateStatusName = "New"
                    }
                };
                await sut.AddCanidateToCollection(documentLink, documentClient, candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 100 rows to DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public async Task Add1000CandidatesToDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                var candidate = new Candidate()
                {
                    CandidateFirstName = "Aram",
                    CandidateLastName = "Koukia",
                    CandidateId = 1,
                    Status = new CandidateStatus()
                    {
                        CandidateStatusId = 1,
                        CandidateStatusName = "New"
                    }
                };
                await sut.AddCanidateToCollection(documentLink, documentClient, candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 1000 rows to DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public async Task Add10000CandidatesToDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var candidate = new Candidate()
                {
                    CandidateFirstName = "Aram",
                    CandidateLastName = "Koukia",
                    CandidateId = 1,
                    Status = new CandidateStatus()
                    {
                        CandidateStatusId = 1,
                        CandidateStatusName = "New"
                    }
                };
                await sut.AddCanidateToCollection(documentLink, documentClient, candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 1000 rows to DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesByIdFromDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            var result = sut.GetCandidateById(1);
            stopWatch.Stop();
            Trace.WriteLine("read candidate by Id from DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesListFromDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            var result = sut.GetCandidatesList(1000);
            stopWatch.Stop();
            Trace.WriteLine("read 1000 candidates list from DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesByNameFromDocumentDB()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = sut.GetDocumentLink();
            var documentClient = sut.GetClient();

            stopWatch.Start();
            var result = sut.GetCandidatesByName("Aram");
            stopWatch.Stop();
            Trace.WriteLine("read 50 candidates by Name from DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }
    }
}
