namespace App.AmazonS3.Models
{
    public class S3ObjectDto
    {
        public required string Name { get; set; } // The name of the S3 object
        public required string PresignedUrl { get; set; } // Pre-signed URL for accessing the object
        public required long Size { get; set; } // Size of the object in bytes
        public required DateTime LastModified { get; set; } // Last modified date of the object
        public required string ETag { get; set; } // ETag of the object
        public string? StorageClass { get; set; } // Storage class of the object (e.g., STANDARD, GLACIER)
        public required string ContentType { get; set; } // MIME type of the object
        public Dictionary<string, string>? Metadata { get; set; } // Metadata associated with the object
    }
}
