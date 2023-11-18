using app.Options;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Memory;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
internal static class SemanticKernelExtensions
{
  public static IServiceCollection AddSemanticKernel(this IServiceCollection services)
  {
    // Semantic Kernel
    services.AddScoped(
        sp =>
        {
          var OpenAIOptions = sp.GetRequiredService<IOptions<OpenAIOptions>>().Value;

          var builder = new KernelBuilder();
          builder.WithLoggerFactory(sp.GetRequiredService<ILoggerFactory>());
          builder.WithOpenAIChatCompletionService(OpenAIOptions.TextModel, OpenAIOptions.ApiKey);
          var kernel = builder.Build();
          return kernel;
        });
    return services;
  }

  public static IServiceCollection AddSemanticMemory(this IServiceCollection services)
  {
    services.AddScoped(sp =>
    {
      var OpenAIOptions = sp.GetRequiredService<IOptions<OpenAIOptions>>().Value;
      var memoryBuilder = new MemoryBuilder();
      memoryBuilder.WithLoggerFactory(sp.GetRequiredService<ILoggerFactory>());
      memoryBuilder.WithOpenAITextEmbeddingGenerationService(OpenAIOptions.EmbeddingModel, OpenAIOptions.ApiKey);
      memoryBuilder.WithMemoryStore(new VolatileMemoryStore());    
      return memoryBuilder.Build();
    });

    return services;
  }

  public static IServiceCollection AddOptions(this IServiceCollection services, ConfigurationManager configuration)
  {
    services
    .AddOptions<DocumentMemoryOptions>()
    .Bind(configuration.GetSection(DocumentMemoryOptions.PropertyName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

    services
    .AddOptions<OpenAIOptions>()
    .Bind(configuration.GetSection(OpenAIOptions.PropertyName))
    .ValidateDataAnnotations()
    .ValidateOnStart();
    
    return services;
  }

  public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
  {
    string[] allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    if (allowedOrigins.Length > 0)
    {
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
               policy =>
               {
                policy.WithOrigins(allowedOrigins)
                       .WithMethods("POST", "GET", "PUT", "DELETE", "PATCH")
                       .AllowAnyHeader();
              });
      });
    }

    return services;
  }
}