using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.FileManager.AmazonS3FileProvider;
using Syncfusion.EJ2.FileManager.Base;

namespace App.AmazonS3.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    public class AmazonS3ProviderController : ControllerBase
    {
        private readonly AmazonS3FileProvider _operation;
        private readonly string _basePath;
        private readonly string _bucketName;
        private readonly string _awsAccessKeyId;
        private readonly string _awsSecretAccessKey;
        private readonly string _region;

        public AmazonS3ProviderController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _basePath = hostingEnvironment.ContentRootPath.Replace("../", "");
            _bucketName = configuration["AWS:BucketName"];
            _awsAccessKeyId = configuration["AWS:AccessKeyId"];
            _awsSecretAccessKey = configuration["AWS:SecretAccessKey"];
            _region = configuration["AWS:Region"];

            _operation = new AmazonS3FileProvider();
            _operation.RegisterAmazonS3(_bucketName, _awsAccessKeyId, _awsSecretAccessKey, _region);
        }

        // Processing the File Manager operations.
        [Route("AmazonS3FileOperations")]
        [HttpPost]
        public object AmazonS3FileOperations([FromBody] FileManagerDirectoryContent args)
        {
            if (args.Action == "delete" || args.Action == "rename")
            {
                if ((args.TargetPath == null) && (args.Path == ""))
                {
                    FileManagerResponse response = new FileManagerResponse();
                    ErrorDetails er = new ErrorDetails
                    {
                        Code = "401",
                        Message = "Restricted to modify the root folder."
                    };
                    response.Error = er;
                    return _operation.ToCamelCase(response);
                }
            }
            switch (args.Action)
            {
                case "read":
                    return _operation.ToCamelCase(_operation.GetFiles(args.Path, false, args.Data));
                case "delete":
                    return _operation.ToCamelCase(_operation.Delete(args.Path, args.Names, args.Data));
                case "copy":
                    return _operation.ToCamelCase(_operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData, args.Data));
                case "move":
                    return _operation.ToCamelCase(_operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData, args.Data));
                case "details":
                    return _operation.ToCamelCase(_operation.Details(args.Path, args.Names, args.Data));
                case "create":
                    return _operation.ToCamelCase(_operation.Create(args.Path, args.Name, args.Data));
                case "search":
                    return _operation.ToCamelCase(_operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive, args.Data));
                case "rename":
                    return _operation.ToCamelCase(_operation.Rename(args.Path, args.Name, args.NewName, false, args.ShowFileExtension, args.Data));
            }
            return null;
        }

        [HttpPost("AmazonS3Upload")]
        public IActionResult AmazonS3Upload(string path, IList<IFormFile> uploadFiles, string action, string data)
        {
            FileManagerResponse uploadResponse;
            FileManagerDirectoryContent[] dataObject = new FileManagerDirectoryContent[1];
            dataObject[0] = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(data);
            foreach (var file in uploadFiles)
            {
                var folders = (file.FileName).Split('/');
                if (folders.Length > 1)
                {
                    for (var i = 0; i < folders.Length - 1; i++)
                    {
                        if (!_operation.checkFileExist(path, folders[i]))
                        {
                            _operation.ToCamelCase(_operation.Create(path, folders[i], dataObject));
                        }
                        path += folders[i] + "/";
                    }
                }
            }
            uploadResponse = _operation.Upload(path, uploadFiles, action, dataObject);
            if (uploadResponse.Error != null)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
            }
            return Content("");
        }

        [HttpGet("AmazonS3Download")]
        public IActionResult AmazonS3Download(string downloadInput)
        {
            FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
            return _operation.Download(args.Path, args.Names);
        }

        [HttpGet("AmazonS3GetImage")]
        public IActionResult AmazonS3GetImage(FileManagerDirectoryContent args)
        {
            return _operation.GetImage(args.Path, args.Id, false, null, args.Data);
        }
    }
}
