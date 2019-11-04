using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Services
{
    public interface IPhotoService
    {
        Task<ApiPhotos> GetApiPhotosAsync();
    }
}