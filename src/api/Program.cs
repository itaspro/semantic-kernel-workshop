using app.Options;
using app.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddConfiguration();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddOptions(builder.Configuration);

// Configure and add semantic services
builder.Services.AddSemanticKernel();
builder.Services.AddSemanticMemory();
builder.Services.AddScoped<PdfDocumentService>();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Redirect root URL to Swagger UI URL
    app.MapWhen(
        context => context.Request.Path == "/",
        appBuilder =>
            appBuilder.Run(
                async context => await Task.Run(() => context.Response.Redirect("/swagger"))));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
