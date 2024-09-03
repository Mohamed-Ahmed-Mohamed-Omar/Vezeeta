namespace Vezeeta.Service.Interface
{
    public interface IFileRepository
    {
        public Task<string> UploadFile(string Location, IFormFile file);

        public Task<bool> RemoveFile(string FolderName, string RemoveFileName);
    }
}
