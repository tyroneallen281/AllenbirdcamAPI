using LinqKit;
using ABC.Domain.Entities;
using ABC.Domain.Models;
using ABC.DomainService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.DomainService.Services
{
    public sealed class FileService : IFileService
    {
        private readonly IConfiguration Configuration;

        public FileService()
        {
        
        }

        public async Task<FileModel> SaveFileAsync(string name, string base64, string container)
        {
            var stream = new MemoryStream();
            stream = await Base64FileToStream(base64);
            await UploadPictureToBlob(name, stream, container);
            var filePath = AppSettings.BlobUrl + container + "/" + name;
            return  new FileModel()
            {
                ContentType = "",
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Path = filePath,
                Size = base64.Length,
            };
        }

        public static async Task<MemoryStream> Base64FileToStream(string base64Image)
        {
            var b64 = base64Image.Substring(base64Image.LastIndexOf(",") + 1);
            int mod4 = b64.Length % 4;
            if (mod4 > 0)
            {
                b64 += new string('=', 4 - mod4);
            }
            var bytes = Convert.FromBase64String(b64);

            return new MemoryStream(bytes);
        }

        public static async Task UploadPictureToBlob(string filename, MemoryStream file, string containerReference)
        {
            try
            {
                // Retrieve the storage account from the connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppSettings.StorageConnectionString);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference(containerReference);

                await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);
                file.ToArray();

                // After copying the contents to stream, initialize it's position
                // back to zeroth location

                file.Seek(0, SeekOrigin.Begin);
                // Create or overwrite the "myblob" blob with contents from a local file.
                await blockBlob.UploadFromStreamAsync(file);
            }
            catch (Exception e)
            {

            }
        }
    }
}