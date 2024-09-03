using Vezeeta.Service.Interface;

namespace Vezeeta.Service.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fileStream = File.Create(Path.Combine(path, fileName)))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }

                return $"/{Location}/{fileName}";
            }
            catch (Exception)
            {
                return "FailedToUploadImage";
            }
        }

        public async Task<bool> RemoveFile(string FolderName, string RemoveFileName)
        {
            if (string.IsNullOrEmpty(RemoveFileName))
            {
                return false;
            }

            // Get the full path to the file
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, FolderName, RemoveFileName);

            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                // Delete the file
                System.IO.File.Delete(filePath);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
