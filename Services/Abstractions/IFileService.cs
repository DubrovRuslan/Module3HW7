namespace Module3HW7.Services.Abstractions
{
    public interface IFileService
    {
        public Task WriteAsync(string text);
        public void ClearOldBackupDirectoryOrCreate();
    }
}
