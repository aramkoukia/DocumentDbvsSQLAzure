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
        private static string EndpointUrl = "https://aramdocumentdb.documents.azure.com:443/";
        private static string AuthorizationKey = "zG+UxBwYg42C9im/vvpehXOr3EIK4em2JRgrr9OsGzdpk5z5JmDd5+hQ+gwSTkPUfNaKLI4BcEnFAkTstT9WaA==";
        private static string DatabaseName = "InterviewDB";
        private static string DocumentCollectionName = "InterviewCollection";

        public async Task<DocumentCollection> CreateDatabaseAndDocumentCollection()
        {
           
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);


            Database database = await client.CreateDatabaseAsync(new Database { Id = DatabaseName });

            DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                                                                                                new DocumentCollection { Id = DocumentCollectionName }
                                                                                               );
            return documentCollection;
        }

        public string GetDocumentLink()
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();
            return documentCollection.DocumentsLink;
        }
        public DocumentClient GetClient()
        {
            return new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
        }

        public async Task<ResourceResponse<Document>> AddCanidateToCollection(string documentLink,DocumentClient client, Candidate candidate)
        {
            return await client.CreateDocumentAsync(documentLink, candidate);
        }

        public List<Candidate> GetCandidatesCollection()
        {
            FeedOptions feedOptions = new FeedOptions()
            {
                MaxItemCount = 100
            };
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return client.CreateDocumentQuery<Candidate>(documentCollection.DocumentsLink, feedOptions).ToList();
        }

        public List<Candidate> GetCandidateById(int candidateId)
        {
            FeedOptions feedOptions = new FeedOptions()
            {
                MaxItemCount = 1
            };
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink, feedOptions).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return client.CreateDocumentQuery<Candidate>(documentCollection.DocumentsLink, feedOptions).Where(m => m.CandidateId == candidateId).ToList();
        }

        public List<Candidate> GetCandidatesByName(string candidateName)
        {
            FeedOptions feedOptions = new FeedOptions()
            {
                MaxItemCount = 50,
            };
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return client.CreateDocumentQuery<Candidate>(documentCollection.DocumentsLink,feedOptions).Where(m => m.CandidateFirstName == candidateName).ToList();
        }

        public List<Candidate> GetCandidatesList(int pageSize)
        {
            FeedOptions feedOptions = new FeedOptions()
            {
                MaxItemCount = pageSize
            };
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return client.CreateDocumentQuery<Candidate>(documentCollection.DocumentsLink,feedOptions).ToList();
        }

    }
}
