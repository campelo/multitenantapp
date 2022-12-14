var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AppConstantsSingleton.Init(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMultiTenantRepository(builder.Configuration)
    .AddMultiTenantCore()
    .AddMultiTenant();

var app = builder.Build();

app.UseMultiTenant();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.SeedDbContext();

app.Run();
