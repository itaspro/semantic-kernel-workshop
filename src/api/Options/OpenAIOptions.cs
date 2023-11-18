using System.ComponentModel.DataAnnotations;

namespace app.Options;

public class OpenAIOptions
{
  public const string PropertyName = "OpenAI";
  
  [Required(AllowEmptyStrings = false)]
  public string TextModel { get; set; } = string.Empty;
  [Required(AllowEmptyStrings = false)]
  public string EmbeddingModel { get; set; } = string.Empty;
  public int MaxRetries { get; set; } = 10;
  [Required(AllowEmptyStrings = false)]
  public string ApiKey { get; set; } = string.Empty;

  public string OrgId { get; set; } = string.Empty;
}