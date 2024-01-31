using MarteliveryAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Services.UserServices;
using MarteliveryAPI.Services.EmailServices;
using MarteliveryAPI.CustomTokenProviders;
using Microsoft.CodeAnalysis.Options;
using System.Security.Policy;
using System.Reflection;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//KeyVault
var keyVaultURL = new Uri(builder.Configuration.GetSection("KeyVaultURL").Value!);
var credential = new DefaultAzureCredential();

builder.Configuration.AddAzureKeyVault(keyVaultURL, credential);

var client = new SecretClient(keyVaultURL, credential);

//DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(client.GetSecret("ConnectionString").Value.Value.ToString());
});

//Email Service
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

//Dependency Injection
builder.Services.AddScoped<IEmailSender, EmailSender>();

//Stripe Configuration
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

//CORS to allow any origin, any method and any header
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

/*******************************/
/*Identity & JWT Authentication*/
/*******************************/
//Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    //Password Settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    //Lockout Settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    //User Settings
    options.User.RequireUniqueEmail = true;

    //SignIn Settings
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    //Token Settings
    options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
})
    .AddEntityFrameworkStores<DataContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation")
    .AddRoles<IdentityRole>();

builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromDays(1));

//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.GetSecret("JWTKey").Value.Value.ToString()))
};
});

//Add Authentication to Swagger UI
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Type = SecuritySchemeType.ApiKey,
    });

    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "MarteliveryAPI", 
        Version = "v1",
        Description = "Martelivery API\n" +
        "\n## Authentication" +
        "\n* Register a new account to the website (Customer or Carrier)" +
        "\n* Log into a user account" +
        "\n* Request a new confirmation email" +
        "\n## Admins 🔒" +
        "\n* **Carrier Rating**" +
        "\n  * Can view all **Carrier Ratings**" +
        "\n  * Can view a single **Carrier Rating** (by searching using its id)" +
        "\n  * Can create a **Carrier Rating**" +
        "\n  * Can update a **Carrier Rating**" +
        "\n  * Can delete a **Carrier Rating**" +
        "\n* **Delivery**" +
        "\n  * Can view all **Deliveries**" +
        "\n  * Can view a single **Delivery** (by searching using its id)" +
        "\n  * Can create a **Delivery**" +
        "\n  * Can update a **Delivery**" +
        "\n  * Can delete a **Delivery** if it is not linked to a **Carrier Rating**" +
        "\n* **Parcel**" +
        "\n  * Can view all **Parcels**" +
        "\n  * Can view a single **Parcel** (by searching using its id)" +
        "\n  * Can create a **Parcel**" +
        "\n  * Can update a **Parcel**" +
        "\n  * Can delete a **Parcel** if it is not linked to a **Quote**" +
        "\n* **Quote**" +
        "\n  * Can view all **Quotes**" +
        "\n  * Can view a single **Quote** (by searching using its id)" +
        "\n  * Can create a **Quote**" +
        "\n  * Can update a **Quote**" +
        "\n  * Can delete a **Quote** if it is not linked to a **Delivery**" +
        "\n* **User**" +
        "\n  * Can view all **Users**" +
        "\n  * Can view a single **User** (by searching using its id)" +
        "\n  * Can update a **User**" +
        "\n  * Can delete a **User** if it is not linked to a **Parcel (Customer)** or a **Quote (Carrier)**" +
        "\n## Carriers 🔒" +
        "\n* **Carrier Rating**" +
        "\n  * Can view all **Carrier Ratings** made by **Customers** to **Deliveries** made by the logged in **Carrier**" +
        "\n* **Delivery**" +
        "\n  * Can view all **Deliveries** made by the logged in **Carrier**" +
        "\n  * Can create a **Delivery** only if his **Quote** is \"Accepted\"" +
        "\n  * Can update his own **Delivery** only if the status is not \"Delivered\"" +
        "\n* **Parcel**" +
        "\n  * Can view all **Parcels** that are not linked to a **Quote** with the status \"Accepted\"" +
        "\n* **Quote**" +
        "\n  * Can view all **Quotes** made by the logged in **Carrier**" +
        "\n  * Can create a **Quote** only if the linked **Parcel** doesn't have a **Quote** with the status \"Accepted\"" +
        "\n  * Can update his own **Quote** only if the status is \"Pending\"" +
        "\n* **User**" +
        "\n  * Can view his own informations" +
        "\n  * Can update his own informations" +
        "\n  * Can update his own password" +
        "\n## Customers 🔒" +
        "\n* **Carrier Rating**" +
        "\n  * Can view all **Carrier Ratings** made by the logged in **Customer** to **Deliveries** linked to **Quotes** he's \"Accepted\" for his own **Parcels**" +
        "\n  * Can create a **Carrier Rating** only if the linked **Delivery** is \"Delivered\" and is linked to **Quote** he's \"Accepted\" for his own **Parcel**" +
        "\n  * Can update his own **Carrier Rating**" +
        "\n  * Can delete his own **Carrier Rating**" +
        "\n* **Delivery**" +
        "\n  * Can view all **Deliveries** linked to **Quotes** he's \"Accepted\" for his own **Parcels**" +
        "\n* **Parcel**" +
        "\n  * Can view all **Parcels** created by the logged in **Customer**" +
        "\n  * Can create a **Parcel**" +
        "\n  * Can update his own **Parcel** only if it is not linked to a **Quote**" +
        "\n  * Can delete his own **Parcel** only if it is not linked to a **Quote**" +
        "\n* **Quote**" +
        "\n  * Can view all **Quotes** linked to the **Parcels** of the logged in **Customer**" +
        "\n  * Can \"Accept\" a **Quote** only if his linked **Parcel** doesn't have another **Quote** with the status \"Accepted\"" +
        "\n* **User**" +
        "\n  * Can view his own informations" +
        "\n  * Can update his own informations" +
        "\n  * Can update his own password",
        Contact = new OpenApiContact
        {
            Name = "Romain Ledda",
            Email = "romainledda@hotmail.com",
            Url = new Uri("https://github.com/Kuhzlol"),
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Dependency Injection
builder.Services.AddScoped<IUser, UserService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseSwagger(options =>
{
    options.RouteTemplate = "MarteliveryAPI/{documentName}/swagger.json";
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/MarteliveryAPI/v1/swagger.json", "MarteliveryAPI");
    options.RoutePrefix = "MarteliveryAPI";
    options.DocumentTitle = "MarteliveryAPI - Swagger UI";
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
