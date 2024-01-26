
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.Repositories.MoneyOrder;
using PostOffice.API.Repositories.OrderStatus;
using PostOffice.API.Repositories.ParcelOrder;
using PostOffice.API.Repositories.ParcelServciePrice;
using PostOffice.API.Repositories.ParcelService;
using PostOffice.API.Repositories.ParcelServicePrice;
using PostOffice.API.Repositories.ParcelType;
using PostOffice.API.Repositories.WeightScope;
using PostOffice.API.Repositorities.MoneyOrder;
using PostOffice.API.Repositorities.MoneyScope;
using PostOffice.API.Repositorities.MoneyServicePrice;
using PostOffice.API.Repositorities.Pincode;
using PostOffice.API.Repositorities.User;
using PostOffice.API.Repositorities.WeightScope;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
// For Entity Framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultDatabase")));

// For Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;     

})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Declare DI
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();


//Add Config for Required Email
builder.Services.Configure<IdentityOptions>(
    options => options.SignIn.RequireConfirmedEmail = false     //set true if required confirm mail after register
    );

builder.Services.Configure<DataProtectionTokenProviderOptions>(options
    => options.TokenLifespan = TimeSpan.FromHours(10));
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
        ClockSkew = TimeSpan.Zero
    };
});


//automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//add email config


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IServicePriceRepository, ServicePriceService>();
builder.Services.AddScoped<IParcelOrderRepository, ParcelOrderService>();
builder.Services.AddScoped<IParcelTypeRepository, ParcelTypeService>();
builder.Services.AddScoped<IWeightScopeRepository, WeightScopeService>();
builder.Services.AddScoped<IParcelServiceRepository, ParcelServiceService>();
builder.Services.AddScoped<IOrderStatusRepository, OrderStatusService>();
builder.Services.AddScoped<IMoneyOrderRepository, MoneyOrderRepository>();
builder.Services.AddScoped<IMoneyServiceRepository, MoneyServiceRepository>();
builder.Services.AddScoped<IMoneyScopeRepository, MoneyScopeRepository>();
builder.Services.AddScoped<IPincodeRepository, PincodeRepository>();
builder.Services.AddScoped<IMoneyServiceRepository, MoneyServiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();


builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//CORS
builder.Services.AddCors(options => options.AddPolicy(name: "FrontEndUI",
          policy =>
          {
              policy.WithOrigins("https://localhost:7077").AllowAnyMethod().AllowAnyHeader();
          }
   ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseCors("FrontEndUI");

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
