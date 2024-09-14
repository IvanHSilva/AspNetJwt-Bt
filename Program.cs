using JwtAspNet.Models;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();
app.MapGet("/", (TokenService service) => 
    {
        User user = new(1, "Ivan Henriques", "ivanhenriques@gmail.com",
            "ihs.jpg", "abdefghi", ["student", "premium"]);
        return service.CreateToken(user);
    }
);

app.Run();
