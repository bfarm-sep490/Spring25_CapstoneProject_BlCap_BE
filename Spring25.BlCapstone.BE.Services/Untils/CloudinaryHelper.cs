using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using System;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Untils
{
    public static class CloudinaryHelper
    {
        private static readonly Cloudinary _cloud;

        static CloudinaryHelper()
        {
            var cloudinaryConfig = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

            if(string.IsNullOrEmpty(cloudinaryConfig))
            {
                throw new Exception("Cloudinary config is missing ???");
            }

            _cloud = new Cloudinary(cloudinaryConfig);
        }

        public const string IMAGE_FOLDER = "IMAGES";
        public const string DOCUMENT_FOLDER = "DOCUMENTS";

        public static async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null...");

            using var stream = file.OpenReadStream();
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + file.FileName;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = IMAGE_FOLDER,
                Overwrite = true,
                UseFilename = false
            };

            var uploadResult = await _cloud.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Upload error: {uploadResult.Error.Message}");

            return uploadResult.SecureUrl.ToString();
        }

        public static async Task<string> UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null...");

            using var stream = file.OpenReadStream();
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + file.FileName;

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = DOCUMENT_FOLDER,
                Overwrite = false,
                UseFilename = false
            };

            var uploadResult = await _cloud.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Upload error: {uploadResult.Error.Message}");
            
            return uploadResult.SecureUrl.ToString();
        }

        public static async Task<List<string>> UploadMultipleImages(List<IFormFile> files)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                var url = await UploadImage(file);
                urls.Add(url);
            }

            return urls;
        }

        public static async Task<List<string>> UploadMultipleDocuments(List<IFormFile> files)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                var url = await UploadDocument(file);
                urls.Add(url);
            }

            return urls;
        }
    }
}
