using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Services
{
    public interface IAdviceService
    {
        Task<GreatAdvice> GetAdviceAsync();
    }
}