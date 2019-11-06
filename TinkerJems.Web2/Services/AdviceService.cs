using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Services
{
    public class AdviceService : IAdviceService
    {
        public async Task<GreatAdvice> GetAdviceAsync()
        {
            var client = new RestClient("https://api.adviceslip.com/advice");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request).Content;
            var selectedResponse = JObject.Parse(response)["slip"].ToString();

            GreatAdvice advice = JsonConvert.DeserializeObject<GreatAdvice>(selectedResponse);

            return advice;
        }
    }
}
