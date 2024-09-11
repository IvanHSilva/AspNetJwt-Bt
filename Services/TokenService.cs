using System.IdentityModel.Tokens.Jwt;

namespace JwtAspNet.Services; 

public class TokenService {
    
    public string CreateToken(){
        
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken token = handler.CreateToken();
        return handler.WriteToken(token);
    }
}
