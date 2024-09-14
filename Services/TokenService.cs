using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using JwtAspNet.Models;

namespace JwtAspNet.Services; 

public class TokenService {
    
    public string CreateToken(){
        
        JwtSecurityTokenHandler handler = new();
        
        byte[] key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

        SigningCredentials credentials = new(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.Sha256);

        SecurityTokenDescriptor descriptor = new(){
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2)
        };

        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user) {
        
        ClaimsIdentity claims = new();
        claims.AddClaim(new Claim("Id", user.Id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claims.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        claims.AddClaim(new Claim("Image", user.Image));

        foreach (string role in user.Roles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}
