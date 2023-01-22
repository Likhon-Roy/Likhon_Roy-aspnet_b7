using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace S3Bucket
{
    public class S3BucketService
    {
        private const string bucketName = "aspnetb7-likhon";
        private readonly RegionEndpoint _bucketRegion = RegionEndpoint.USEast1;
        private IAmazonS3 client;

        public S3BucketService()
        {
            client = new AmazonS3Client(_bucketRegion);
        }

        public S3BucketService(RegionEndpoint bucketRegion)
        {
            client = new AmazonS3Client(bucketRegion);
            _bucketRegion = bucketRegion;
        }

        public async Task DeleteFile(string keyName)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                Console.WriteLine("Deleting an object");
                await client.DeleteObjectAsync(deleteObjectRequest);
                Console.WriteLine("Object Deleted");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
        }

        public async Task DownloadFile(string keyName, string downloadPath)
        {
            //string responseBody = "";
            MemoryStream? ms = null;
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (var response = await client.GetObjectAsync(request))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", "fileName"));

                System.IO.File.WriteAllBytes(downloadPath, ms.ToArray());

                Console.WriteLine("File downloaded successfully to this path "+downloadPath);

                //using (GetObjectResponse response = await client.GetObjectAsync(request))
                //using (Stream responseStream = response.ResponseStream)
                //using (StreamReader reader = new StreamReader(responseStream))
                //{
                //    string title = response.Metadata["image"]; // Assume you have "title" as medata added to the object.
                //    string contentType = response.Headers["image"];
                //    Console.WriteLine("Object metadata, Title: {0}", title);
                //    Console.WriteLine("Content type: {0}", contentType);

                //    responseBody = reader.ReadToEnd(); // Now you process the response body.
                //}
            }
            catch (AmazonS3Exception e)
            {
                // If bucket or object does not exist
                Console.WriteLine("Error encountered ***. Message:'{0}' when reading object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when reading object", e.Message);
            }
        }

        public async Task UploadFile(string keyName, string filePath)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(client);

                // Use TransferUtilityUploadRequest to configure options.
                // In this example we subscribe to an event.
                var uploadRequest =
                    new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        FilePath = filePath,
                        Key = keyName
                    };

                uploadRequest.UploadProgressEvent += new EventHandler<UploadProgressArgs>(uploadRequest_UploadPartProgressEvent);

                await fileTransferUtility.UploadAsync(uploadRequest);
                Console.WriteLine("Upload completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        static void uploadRequest_UploadPartProgressEvent(object sender, UploadProgressArgs e)
        {
            // Process event.
            Console.WriteLine("{0}/{1}", e.TransferredBytes, e.TotalBytes);
        }
    }
}