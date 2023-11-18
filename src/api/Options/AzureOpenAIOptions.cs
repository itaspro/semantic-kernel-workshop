using System.ComponentModel.DataAnnotations;

namespace app.Options;

public class AzureOpenAIOptions
{
  public const string PropertyName = "AzureOpenAI";
  
  [Required(AllowEmptyStrings = false)]
  public string TextModel { get; set; } = string.Empty;

  [Required(AllowEmptyStrings = false)]
  public string EmbeddingModel { get; set; } = string.Empty;

  public int MaxRetries { get; set; } = 10;

  [Required(AllowEmptyStrings = false)]
  public string ApiKey { get; set; } = string.Empty;

  [Url]
  public string Endpoint { get; set; } = string.Empty;
}