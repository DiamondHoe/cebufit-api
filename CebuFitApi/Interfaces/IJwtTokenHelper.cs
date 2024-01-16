namespace CebuFitApi.Interfaces
{
    public interface IJwtTokenHelper
    {
        public Task<string> GenerateJwtToken(Guid userId, string username);
    }
}
