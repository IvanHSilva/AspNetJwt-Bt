using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAspNet.Services; 

public class TokenService {
    
    public string CreateToken(){
        
        JwtSecurityTokenHandler handler = new();
        
        byte[] key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

        new SigningCredentials(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.Sha256);

        JwtSecurityToken token = handler.CreateToken();
        return handler.WriteToken(token);
    }
}
