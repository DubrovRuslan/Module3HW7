namespace Module3HW7.Services.Abstractions
{
    public interface IBackupService
    {
        public Task CreateBackupAsync(string text);
    }
}
