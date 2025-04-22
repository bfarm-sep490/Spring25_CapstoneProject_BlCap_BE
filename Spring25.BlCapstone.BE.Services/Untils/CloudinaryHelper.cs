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
        public const string QRCODE_FOLDER = "QRCODES";

        public static async Task<(string PublicId, string Url)> UploadImage(IFormFile file)
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

            return (uploadResult.PublicId, uploadResult.SecureUrl.ToString());
        }

        public static async Task<(string PublicId, string Url)> UploadImageQRCode(byte[] imageData, string fileName = null)
        {
            if (imageData == null || imageData.Length == 0)
                throw new ArgumentException("Image data is empty or null...");

            fileName ??= DateTime.Now.ToString("yyyyMMddHHmmss") + "_qr.png";
            using var stream = new MemoryStream(imageData);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = QRCODE_FOLDER,
                Overwrite = true,
                UseFilename = false
            };

            var uploadResult = await _cloud.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Upload error: {uploadResult.Error.Message}");

            return (uploadResult.PublicId, uploadResult.SecureUrl.ToString());
        }

        public static async Task<(string PublicId, string Url)> UploadDocument(IFormFile file)
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
            
            return (uploadResult.PublicId, uploadResult.SecureUrl.ToString());
        }

        public static async Task<List<(string PublicId, string Url)>> UploadMultipleImages(List<IFormFile> files)
        {
            var resultList = new List<(string PublicId, string Url)>();

            foreach (var file in files)
            {
                var (publicId, url) = await UploadImage(file);
                resultList.Add((publicId, url));
            }

            return resultList;
        }

        public static async Task<List<(string PublicId, string Url)>> UploadMultipleDocuments(List<IFormFile> files)
        {
            var resultList = new List<(string PublicId, string Url)>();

            foreach (var file in files)
            {
                var (publicId, url) = await UploadDocument(file);
                resultList.Add((publicId, url));
            }

            return resultList;
        }

        public static async Task<bool> DeleteImage(string publicId)
        {
            try
            {
                var deletionParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image
                };

                var deletionResult = await _cloud.DestroyAsync(deletionParams);

                if (deletionResult.Result == "ok")
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Failed to delete image: {deletionResult.Error?.ToString()}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting image from Cloudinary: {ex.Message}");
            }
        }

        public static async Task<string> UpdateImage(IFormFile file, string publicId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null...");

            using var stream = file.OpenReadStream();
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + file.FileName;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                PublicId = publicId,   
                Overwrite = true,      
                UseFilename = false
            };

            var uploadResult = await _cloud.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Upload error: {uploadResult.Error.Message}");

            return uploadResult.SecureUrl.ToString(); 
        }

    }
}
