using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace szkolachmury_8
{
    public static class cosmosDbChangeFeed
    {
        [FunctionName("cosmosDbChangeFeedFunction")]
        public static void Run([CosmosDBTrigger(
            databaseName: "db",
            collectionName: "user",
            ConnectionStringSetting = "dbConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified = " + input.Count);
                log.LogInformation("First document Id = " + input[0].Id);
                var userName = input[0].GetPropertyValue<string>("name");
                log.LogInformation("userName = " + userName);
            }
        }
    }
}
