using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
// based on aws s3
namespace ControlNew.Network
{
    public class S3Handler
    {
        private static S3Handler Instance;
        private const string bucketName = "drones-bucket";
        private  string keyName = "";//the file name on S3 
        private const string filePath = @"C:\Users\shai\Desktop\New folder\"; // ""SET REAL FILE PATH

        
        /////////// S3 Info
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest1;
        private const string AccessKeyID = "AKIAJ6UX3AERZQFM5V7A";
        private const string AccessKeyName = "urSINJkfvBB6OgMX81/r7tfz1S/thAu/+6JLZmR1";
        private static IAmazonS3 client;
        

        private S3Handler()
        {
            client = new AmazonS3Client(AccessKeyID, AccessKeyName, bucketRegion);
        }

        public static S3Handler instance
        {
            get
            {
                if(Instance == null)
                {
                    Instance = new S3Handler();
                }
                return Instance;
            }
        }
        //get the file name 
        public async Task<bool> FileUpload(string fileName)
        {
            keyName = fileName;
            return await WritingAnObjectAsync();
            
        }
        //writing an object to S3 server return true if sucsess false if not sucsess
        async Task<bool> WritingAnObjectAsync()
        {
            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath+keyName,
                    ContentType = "image/*"
                };

                PutObjectResponse response = client.PutObject(putRequest);
                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //s3 error hnadler
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
