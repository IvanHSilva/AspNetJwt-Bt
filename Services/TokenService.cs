using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using JwtAspNet.Models;

namespace JwtAspNet.Services; 

public class TokenService {
    
    public string CreateToken(User user){
        
        JwtSecurityTokenHandler handler = new();
        
        byte[] key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

        SigningCredentials credentials = new(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor descriptor = new(){
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = GenerateClaims(user)
        };

        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user) {
        
        ClaimsIdentity claims = new();
        claims.AddClaim(new Claim("id", user.Id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claims.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        claims.AddClaim(new Claim("image", user.Image));

        foreach (string role in user.Roles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}
