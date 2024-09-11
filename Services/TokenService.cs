using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
}
