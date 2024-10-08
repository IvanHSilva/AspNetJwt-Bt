using JwtAspNet;
using JwtAspNet.Extensions;
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
    id = user.Id(),
    name = user.Name(),
    email = user.Email(),
    givenName = user.GivenName(),
    image = user.Image(),
}).RequireAuthorization();
app.MapGet("/admin", () => "Acesso autorizado!").RequireAuthorization("admin");

app.Run();
