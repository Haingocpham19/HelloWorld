using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using App.AmazonS3.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.AmazonS3.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;

        public FilesController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return Ok($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync(string bucketName, string? prefix)
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };

            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };

                var metadataRequest = new GetObjectMetadataRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key
                };
                var metadata = _s3Client.GetObjectMetadataAsync(metadataRequest).Result;

                return new S3ObjectDto()
                {
                    Name = s.Key,
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                    Size = s.Size,
                    LastModified = s.LastModified,
                    ETag = s.ETag,
                    StorageClass = s.StorageClass.Value,
                    ContentType = metadata.Headers.ContentType,
                    Metadata = metadata.Metadata
                        .Keys
                        .ToDictionary(key => key, key => metadata.Metadata[key])
                };
            });
            return Ok(s3Objects);
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFileAsync(string bucketName, string key)
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var request = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = key
            };

            using (var response = await _s3Client.GetObjectAsync(request))
            {
                return File(response.ResponseStream, response.Headers.ContentType, key);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string bucketName, string key)
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

            var request = new DeleteObjectRequest()
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(request);
            return Ok($"File {key} deleted successfully from bucket {bucketName}.");
        }
    }
}
