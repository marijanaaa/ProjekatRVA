using Microsoft.IdentityModel.Tokens;
using ProjekatRVA.Models;
using ProjekatRVA.Tokens.ITokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjekatRVA.Tokens.TokenProviders
{
    public class GenerateJwtToken : IGenerateToken
    {
        public string GenerateToken(User user, SymmetricSecurityKey secretKey)
        {
            List<Claim> claims = new List<Claim>();
            if (user.UserType == Enums.EUserType.ADMIN)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            if (user.UserType == Enums.EUserType.GUEST)
                claims.Add(new Claim(ClaimTypes.Role, "guest"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44386", //url servera koji je izdao token
                claims: claims, //claimovi
                expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                signingCredentials: signinCredentials //kredencijali za potpis
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
