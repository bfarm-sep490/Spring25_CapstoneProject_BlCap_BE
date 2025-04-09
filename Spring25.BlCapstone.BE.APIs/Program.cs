
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Spring25.BlCapstone.BackgroundServices.BackgroundServices;
using Spring25.BlCapstone.BE.APIs.Configs;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.BlockChain;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Redis;
using Spring25.BlCapstone.BE.Repositories.Repositories;
using Spring25.BlCapstone.BE.Services.Services;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
    options.JsonSerializerOptions.WriteIndented = false;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverterWithoutOffset());
    }); ;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidAudience = builder.Configuration["JWT:ValidAudience"],
                       ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))

                   };
               });
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "BlCapstone API Swagger v1", Version = "v1" });
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
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                    },
                        Array.Empty<string>()
                    }
                });

});

//Register Service
builder.Services.AddScoped<IAuthencationService, AuthencationService>();
builder.Services.AddScoped<IFCMService, FCMService>();
builder.Services.AddScoped<IAblyService, AblyService>();
builder.Services.AddScoped<IFarmerService, FarmerService>();
builder.Services.AddScoped<IInspectorService, InspectorService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<IYieldService, YieldService>();
builder.Services.AddScoped<IRetailerService, RetailerService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IPesticideService, PesticideService>();
builder.Services.AddScoped<IFertilizerService, FertilizerService>();
builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ICaringTaskService, CaringTaskService>();
builder.Services.AddScoped<IHarvestingTaskService, HarvestingTaskService>();
builder.Services.AddScoped<IInspectingFormService, InspectingFormService>();
builder.Services.AddScoped<IPackagingTaskService, PackagingTaskService>();
builder.Services.AddScoped<IInspectingResultService, InspectingResultService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPackagingTypeService, PackagingTypeService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IPackagingProductService, PackagingProductService>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddSingleton<RedisManagement>();

//Register Background Service
builder.Services.AddHostedService<TaskCheckStatusService>();
builder.Services.AddHostedService<TaskChangeStatusPlanByOrder>();
builder.Services.AddHostedService<ExpiredProductCheckStatusService>();
builder.Services.AddScoped<IBlockChainService, BlockChainService>();
builder.Services.AddScoped<IVechainInteraction, VechainInteraction>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
