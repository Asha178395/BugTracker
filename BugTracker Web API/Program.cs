using BugTracker_Web_API;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.Google;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var azureTenantID = builder.Configuration["AuthOptions:TenantId"];
    var applicationIdUrl = builder.Configuration["AuthOptions:ApplicationIdURI"];
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{azureTenantID}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"https://login.microsoftonline.com/{azureTenantID}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                            {
                                 { $"{applicationIdUrl}/access_has_user", "userAccess" },
                            },
            },
        },
    });
    // Add the security definition for Google authentication
    c.AddSecurityDefinition("Google", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows 
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                Scopes = { { "openid", "OpenID" }, { "email", "Email" }, { "profile", "Profile" } }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                     {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = JwtBearerDefaults.AuthenticationScheme,
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                     },
                });
    // Configure the security requirements for Google authentication
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Google" },
                            Scheme = "Google",
                            Name = "Google",
                            In = ParameterLocation.Header,
                },
                new[] { "openid", "email", "profile" }
            }
        });
});


// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Google";
})
.AddJwtBearer(options =>
{
    builder.Configuration.Bind("AuthOptions", options);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudiences = AuthenticationHelper.GetValidAudiences(builder.Configuration),
        ValidIssuers = AuthenticationHelper.GetValidIssuers(builder.Configuration),
        AudienceValidator = AuthenticationHelper.AudienceValidator,
    };
})

.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["AuthOptionsGoogle:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["AuthOptionsGoogle:ClientSecret"];
});

/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "wellship_svc_app", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                Scopes = new Dictionary<string, string> { { "email", "email" }, { "profile", "profile" } }
            }
        },
        Extensions = new Dictionary<string, IOpenApiExtension>
        {
            {"x-tokenName", new OpenApiString("id_token")}
        },
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirements = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string> {"email", "profile"}
        }
    };

   c.AddSecurityRequirement(securityRequirements);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["AuthOptionsGoogle:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["AuthOptionsGoogle:ClientSecret"];
    });
*/
/*.AddJwtBearer(o =>
{
    o.IncludeErrorDetails = true;
    o.SecurityTokenValidators.Clear();
    o.SecurityTokenValidators.Add(new GoogleTokenValidator());
    o.Audience = "277449229702-aiuptmtnq5aleo5ouaeklq7f5fdctkrl.apps.googleusercontent.com"; 
    o.Authority = "https://accounts.google.com";
});
*/



var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();
app.MapControllers();
app.Run();