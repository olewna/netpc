using NetPcApi.Models;

namespace NetPcApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}