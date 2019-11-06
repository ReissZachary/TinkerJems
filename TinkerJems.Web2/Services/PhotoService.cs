using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<ApiPhotos> GetApiPhotosAsync()
        {
            var client = new RestClient("http://www.splashbase.co/api/v1/images/random");
            var request = new RestRequest(Method.GET);
            var resposne = client.Execute(request).Content;

            ApiPhotos Photos = JsonConvert.DeserializeObject<ApiPhotos>(resposne);

            return Photos;
        }
    }
}
