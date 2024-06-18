using CebuFitApi.Models;

namespace CebuFitApi.Interfaces
{
    public interface IJwtTokenHelper
    {
        public Task<string> GenerateJwtToken(User user, bool? expire);
        public Guid GetCurrentUserId();

    }
}
