using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccesLayer.Datas;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Repositories;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region DI Services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options
             => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlServer")));


builder.Services.AddTransient<IAdsElonInterface, AdsElonRepository>();
builder.Services.AddTransient<ICategoryInterface, CategoryRepository>();

builder.Services.AddTransient<IChatInterface, ChatRepository>();
builder.Services.AddTransient<IImageInterface, ImageRepository>();

builder.Services.AddTransient<IMessageInterface, MessageRepository>();
builder.Services.AddTransient<IRegionInterface, RegionRepository>();

builder.Services.AddTransient<ISubCategoryInterface, SubCategoryRepository>();
builder.Services.AddTransient<ISubRegionInterface, SubRegionRepository>();

builder.Services.AddTransient<IUserInterface, UserRepository>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
#endregion 

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMepperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
