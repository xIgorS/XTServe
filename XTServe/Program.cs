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
            return userName != null && (userName == "iswin\\igorsedykh" || userName == "iswin\\igorsedykh1");
        }));
});

// Configure CORS policy for XTWeb
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowXTWeb", policy =>
    {
        policy.WithOrigins("https://localhost:59336", "http://localhost:59337")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
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
app.UseCors("AllowXTWeb");

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
