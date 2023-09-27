using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RioUpesi.CORE.DataContext;
using RioUpesi.CORE.Models.Constants;
using RioUpesi.CORE.Models.Users;
using RioUpesi.INFRASTRUCTURE.Iservices.IImagesServices;
using RioUpesi.INFRASTRUCTURE.Iservices.INotifications;
using RioUpesi.INFRASTRUCTURE.Iservices.IRoomServices;
using RioUpesi.INFRASTRUCTURE.Services.ImagesServices;
using RioUpesi.INFRASTRUCTURE.Services.Notifications;
using RioUpesi.INFRASTRUCTURE.Services.RoomServices;
using RiUpesi.INFRASTRUCTURE.Iservices.IUserServices;
using RiUpesi.INFRASTRUCTURE.Services.LoggedInUser;
using RiUpesi.INFRASTRUCTURE.Services.UserServices;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();

var configuration = provider.GetService<IConfiguration>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<RioUsers, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<RioContext>();
builder.Services.AddDbContext<RioContext>(
               x => x.UseSqlServer(configuration.GetConnectionString("DevConnectiions")));
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "RioUpesi.API", Version = "v1" }
    );
    c.AddSecurityDefinition(
      "Bearer",
      new OpenApiSecurityScheme
      {
          In = ParameterLocation.Header,
          Description = "Please Insert token",
          Name = "Authorization",
          Type = SecuritySchemeType.Http,
          BearerFormat = "Jwt",
          Scheme = "bearer"
      }
      );
    c.AddSecurityRequirement(
      new OpenApiSecurityRequirement
     {
            {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "Bearer"}},

            new string[] { }
        }});

});
builder.Services
     .AddAuthentication(opt =>
     {
         opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(opt =>
     {
         opt.RequireHttpsMetadata = true;
         opt.SaveToken = true;
         opt.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY)),
             ValidateIssuer = false,
             ValidateAudience = false
         };
     });

builder.Services.Configure<IdentityOptions>(options =>
options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier
);


builder.Services.AddScoped<IloggedInUser, loggedInUser>();
builder.Services.AddScoped<IImagesServices, ImagesServices>();
builder.Services.AddScoped<IRoomServices, RoomServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<INotificationServices, NotifcationServices>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
