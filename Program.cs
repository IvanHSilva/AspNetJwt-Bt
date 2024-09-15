using JwtAspNet;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();
builder.Services.AddAuthentication(a => {
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(a => {
    a.TokenValidationParameters = new TokenValidationParameters {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", p => p.RequireRole("admin"));

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/Login", (TokenService service) => 
    {
        User user = new(1, "Ivan Henriques", "ivanhenriques@gmail.com",
            "ihs.jpg", "abdefghi", ["student", "premium"]);
        return service.CreateToken(user);
    }
);

app.MapGet("/restrict", (ClaimsPrincipal user) => new {
    id = user.Claims.First(c => c.Type == "id").Value,
    name = user.Claims.First(c => c.Type == ClaimTypes.Name).Value,
    email = user.Claims.First(c => c.Type == ClaimTypes.Email).Value,
    givenName = user.Claims.First(c => c.Type == ClaimTypes.GivenName).Value,
    image = user.Claims.First(c => c.Type == "image").Value,
}).RequireAuthorization();
app.MapGet("/admin", () => "Acesso autorizado!").RequireAuthorization("admin");

app.Run();
