using System.Security.Claims;

namespace JwtAspNet.Extensions; 

public static class ClaimTypesExtension {

    public static int Id(this ClaimsPrincipal user) {

        try {
            string id = user.Claims.First(c => c.Type == "id").Value ?? "0";
            return int.Parse(id);
        } catch { 
            return 0;
        }
    }
    
    public static string Name(this ClaimsPrincipal user) {

        try {
            string name = user.Claims.First(c => c.Type == ClaimTypes.Name).Value ?? "";
            return name;
        } catch {
            return "";
        }
    }

    public static string Email(this ClaimsPrincipal user) {

        try {
            string email = user.Claims.First(c => c.Type == ClaimTypes.Email).Value ?? "";
            return email;
        } catch {
            return "";
        }
    }

    public static string GivenName(this ClaimsPrincipal user) {

        try {
            string givenName = user.Claims.First(c => c.Type == ClaimTypes.GivenName).Value ?? "";
            return givenName;
        } catch {
            return "";
        }
    }

    public static string Image(this ClaimsPrincipal user) {

        try {
            string image = user.Claims.First(c => c.Type == "image").Value ?? "";
            return image;
        } catch {
            return "";
        }
    }
}
