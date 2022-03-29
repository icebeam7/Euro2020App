using System.Threading.Tasks;

namespace Euro2020App.Interfaces
{
    public interface ILocalPath
    {
        Task<string> GetPath();
        Task<string> SaveImage();
    }
}
