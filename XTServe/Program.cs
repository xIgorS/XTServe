var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Windows Authentication
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Negotiate.NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthorizedUsersOnly", policy =>
        policy.RequireAssertion(context =>
        {
            var userName = context.User.Identity?.Name?.ToLowerInvariant();
            return userName == "iswin\\igorsedykh" || userName == "iswin\\igorsedykh1";
        }));
});

// Configure CORS policy for all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable CORS
app.UseCors("AllowAll");

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
