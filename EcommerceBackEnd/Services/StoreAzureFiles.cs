
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace EcommerceBackEnd.Services
{
    public class StoreAzureFiles : IFileStorage
    {
        private readonly string connectionString;
        // we called the configuration as a param to get the connection string of the Azure storage
        public StoreAzureFiles(IConfiguration configuration)
        {
            // connection string from appsetting
            //connectionString = configuration.GetConnectionString("AzureStorage");
            connectionString = configuration["ConnectionStrings:AzureStorage"];
            Console.WriteLine(connectionString);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            // create a new folder if it doesnt exist
            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);

            // add a name to the file and create a new object with its name thats gonna be added to the blob
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHearder = new BlobHttpHeaders();
            blobHttpHearder.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHearder;

            await blob.UploadAsync(new BinaryData(content), blobUploadOptions);
            return blob.Uri.ToString();
        }

        public async Task<string> DeleteFile(string route, string container)
        {
            if (string.IsNullOrEmpty(route)) return "Route is null or empty";

            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();

            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();

            return $"File '{file}' deleted successfully.";
        }

        public async Task<string> EditFile(byte[] contenido, string extension, string container, string route,  string contentType)
        {
            await DeleteFile(route, container);
            return await SaveFile(contenido, extension, container, contentType);
        }

        
    }
}
