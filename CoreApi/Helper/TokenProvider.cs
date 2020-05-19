using CoreApi.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Helper
{
    public class TokenProvider
    {
        //https://medium.com/@kedren.villena/refresh-jwt-token-with-asp-net-core-c-25c2c9ee984b


        private string _tokenkey;
        private string _tokenAudience;
        private string _tokenIssuer;

        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AppUser user)
        {
            _tokenkey = _configuration.GetValue<string>("TokenKey");
            _tokenAudience = _configuration.GetValue<string>("TokenAudiences");
            _tokenIssuer = _configuration.GetValue<string>("TokenIssuer");

            byte[] key = Encoding.ASCII.GetBytes(_tokenkey); ;


            var JWToken = new JwtSecurityToken(
                    issuer: _tokenIssuer,// "https://localhost:44377",
                    audience: _tokenAudience,// "https://localhost:44377",
                    claims: UserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return token;
        }

        public JwtSecurityToken DecodeToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //SecurityToken j = handler.ReadToken(token);
            JwtSecurityToken jwttoken = handler.ReadToken(token) as JwtSecurityToken;
            return jwttoken;
        }

        private static IEnumerable<Claim> UserClaims(AppUser user)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email + ":"+ "Dev"),
                new Claim("UserID", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Role", "Dev")
            };
            return claims;
        }
    }
}
