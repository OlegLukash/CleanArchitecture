using OnlineBookShop.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServices();

//builder.Services.AddAuthentication(builder.Configuration);
//builder.Services.AddControllers();

//builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddApplication();
//builder.Services.AddAutoMapper(typeof(ApplicationAssemblyMarker));
//builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();

await app.SeedData();

// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

await app.RunAsync();
