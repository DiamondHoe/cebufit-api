namespace CebuFitApi.Interfaces
{
    public interface IJwtTokenHelper
    {
        public Task<string> GenerateJwtToken(Guid userId, string username, bool? expire);
        public Guid GetCurrentUserId();

    }
}
