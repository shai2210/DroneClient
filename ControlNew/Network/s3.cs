using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using System.IO;

namespace ControlNew
{
    public class s3
    {
        private const string bucketName = "drones-bucket";
        
        private const string keyName1 = "IMG_0001.jpg";
        private const string filePath = @"C:\Users\shai\Desktop\New folder\IMG_0001.jpg";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest1;

  
        private static IAmazonS3 client;

        public s3()
        {
            client = new AmazonS3Client("AKIAJ6UX3AERZQFM5V7A", "urSINJkfvBB6OgMX81/r7tfz1S/thAu/+6JLZmR1",bucketRegion);
            WritingAnObjectAsync().Wait();
        }
        

        static async Task WritingAnObjectAsync()
        {
            
            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName1,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };


                PutObjectResponse response = client.PutObject(putRequest);
                Console.WriteLine(response.HttpStatusCode);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }

        }

        
    }
}
