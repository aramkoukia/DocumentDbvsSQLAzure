using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocumentDBDemo
{
    public class DocumentDBProvider
    {
        private static string EndpointUrl = "https://querydemo.documents.azure.com:443/";
        private static string AuthorizationKey = "+9x2hFc7QsZ5hReULaqmBs01amCFiQAJZuoTqdZ79h/fGd2RSYoJVXAegVS7suJBg1pB+RQC8D45gp7bk0rSUw==";
        private static string DatabaseName = "InterviewDB";
        private static string DocumentCollectionName = "InterviewCollection";

        private DocumentClient client;
        private DocumentCollection collection;

        public DocumentDBProvider()
        {
            this.client = new DocumentClient(
                new Uri(EndpointUrl),
                AuthorizationKey,
                new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct,
                    ConnectionProtocol = Protocol.Tcp,
                    MaxConnectionLimit = 100,
                    RetryOptions = new RetryOptions { MaxRetryAttemptsOnThrottledRequests = 10 }
                });

            Database database = this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseName }).Result;

            DocumentCollection documentCollection = this.client.CreateDocumentCollectionIfNotExistsAsync(
                database.CollectionsLink,
                new DocumentCollection { Id = DocumentCollectionName },
                new RequestOptions { OfferThroughput = 10000 }).Result;

            this.collection = documentCollection;
        }

        public async Task CleanupDocuments()
        {
            foreach (Document document in client.CreateDocumentQuery(this.collection.SelfLink))
            {
                await client.DeleteDocumentAsync(document.SelfLink);
            }
        }

        public DocumentCollection CreateDatabaseAndDocumentCollection()
        {
            return this.collection;
        }

        public string GetDocumentLink()
        {
            return this.collection.DocumentsLink;
        }

        public DocumentClient GetClient()
        {
            return this.client;
        }

        public async Task<ResourceResponse<Document>> AddCandidateToCollection(string documentLink,DocumentClient client, Candidate candidate)
        {
            return await client.UpsertDocumentAsync(documentLink, candidate);
        }

        public List<Candidate> GetCandidatesCollection()
        {
            return client.CreateDocumentQuery<Candidate>(this.collection.SelfLink).Take(100).ToList();
        }

        public List<Candidate> GetCandidateById(int candidateId)
        {
            return client.CreateDocumentQuery<Candidate>(this.collection.SelfLink).Where(m => m.CandidateId == candidateId).Take(1).ToList();
        }

        public List<Candidate> GetCandidatesByName(string candidateName)
        {
            return client.CreateDocumentQuery<Candidate>(this.collection.SelfLink)
                .Where(m => m.CandidateFirstName == candidateName)
                .Take(50).ToList();
        }

        public List<Candidate> GetCandidatesList(int pageSize)
        {
            return client.CreateDocumentQuery<Candidate>(this.collection.SelfLink).Take(pageSize).ToList();
        }

    }
}
