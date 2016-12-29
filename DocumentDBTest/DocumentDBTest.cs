using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using DocumentDBDemo;
using System.Diagnostics;
using Microsoft.Azure.Documents.Client;

namespace DocumentDBTest
{
    [TestClass]
    public class DocumentDBTest
    {
        private DocumentDBProvider sut;

        private DocumentDBProvider Sut
        {
            get
            {
                if (sut == null)
                {
                    this.sut = new DocumentDBDemo.DocumentDBProvider();
                }

                return this.sut;
            }
        }

        [TestInitialize]
        private void Initialize()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
            this.Sut.CleanupDocuments().Wait();
        }


        [TestMethod]
        public async Task CreateDatabaseAndDocumentCollection_Test()
        {            
            var result = this.Sut.CreateDatabaseAndDocumentCollection();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task AddCanidateToCollection_Test()
        {
            var documentLink = Sut.GetDocumentLink();
            var documentClient = Sut.GetClient();
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
            var result = await Sut.AddCandidateToCollection(documentLink, documentClient, candidate);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetCandidatesCollection_Test()
        {
            var result = Sut.GetCandidatesCollection();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCandidateById_Test()
        {
            var result = Sut.GetCandidateById(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add10CandidatesToDocumentDB()
        {
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = Sut.GetDocumentLink();
            var documentClient = Sut.GetClient();

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
                await Sut.AddCandidateToCollection(documentLink, documentClient, candidate);
            }
            stopWatch.Stop();
            Trace.WriteLine("Add 10 rows to DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        private async Task AddNCandidatesToDocumentDB(int n)
        {
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = Sut.GetDocumentLink();
            var documentClient = Sut.GetClient();

            stopWatch.Start();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < n; i++)
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
                tasks.Add(Sut.AddCandidateToCollection(documentLink, documentClient, candidate));
            }

            await Task.WhenAll(tasks);

            stopWatch.Stop();
            Trace.WriteLine(string.Format("Add {0} rows to DocumentDB took {1}", n,stopWatch.Elapsed));
            Trace.Flush();
        }

        [TestMethod]
        public async Task Add100CandidatesToDocumentDB()
        {
            await AddNCandidatesToDocumentDB(100);
        }

        [TestMethod]
        public async Task Add1000CandidatesToDocumentDB()
        {
            await AddNCandidatesToDocumentDB(1000);
        }

        [TestMethod]
        public async Task Add10000CandidatesToDocumentDB()
        {
            await AddNCandidatesToDocumentDB(10000);
        }

        [TestMethod]
        public void ReadCandidatesByIdFromDocumentDB()
        {
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = Sut.GetDocumentLink();
            var documentClient = Sut.GetClient();

            stopWatch.Start();
            var result = Sut.GetCandidateById(1);
            stopWatch.Stop();
            Trace.WriteLine("read candidate by Id from DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesListFromDocumentDB()
        {
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = Sut.GetDocumentLink();
            var documentClient = Sut.GetClient();

            stopWatch.Start();
            var result = Sut.GetCandidatesList(1000);
            stopWatch.Stop();
            Trace.WriteLine("read 1000 candidates list from DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }

        [TestMethod]
        public void ReadCandidatesByNameFromDocumentDB()
        {
            Stopwatch stopWatch = new Stopwatch();
            var documentLink = Sut.GetDocumentLink();
            var documentClient = Sut.GetClient();

            stopWatch.Start();
            var result = Sut.GetCandidatesByName("Aram");
            stopWatch.Stop();
            Trace.WriteLine("read 50 candidates by Name from DocumentDB took:" + stopWatch.Elapsed);
            Trace.Flush();
        }
    }
}
