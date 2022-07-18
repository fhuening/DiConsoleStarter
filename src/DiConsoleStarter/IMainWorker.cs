using System.Threading.Tasks;

namespace DiConsoleStarter
{
    public interface IMainWorker
    {
        Task ExecuteAsync(string[] args);
    }
}