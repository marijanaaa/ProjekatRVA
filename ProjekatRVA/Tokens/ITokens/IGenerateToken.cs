using Microsoft.IdentityModel.Tokens;
using ProjekatRVA.Models;

namespace ProjekatRVA.Tokens.ITokens
{
    public interface IGenerateToken
    {
        string GenerateToken(User user, SymmetricSecurityKey secretKey);
    }
}
