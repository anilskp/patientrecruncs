using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace patientrecruncs
{
    public static class OnBlobCreated
    {
        [FunctionName("OnBlobCreated")]
        public static void Run(
            [BlobTrigger("patient/{name}", Connection = "AzureWebJobsStorage")] String myBlob, string name,
            [CosmosDB(
                databaseName: "AudioTXT",
                collectionName: "Patient",
                ConnectionStringSetting = "CosmosDBConnection")]
                IAsyncCollector<String> patientRecord,
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob} Bytes");

            patientRecord.AddAsync(myBlob);

        }
    }
}
