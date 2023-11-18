
using System.ComponentModel.DataAnnotations;

namespace app.Options;

public class DocumentMemoryOptions {
  public const string PropertyName = "DocumentMemory";
 
  /// <summary>
  /// Gets or sets the maximum number of tokens to use when splitting a document into "lines".
  /// For more details on tokens and how to count them, see:
  /// https://help.openai.com/en/articles/4936856-what-are-tokens-and-how-to-count-them
  /// </summary>
  [Range(0, int.MaxValue)]
  public int DocumentLineSplitMaxTokens { get; set; } = 30;

  /// <summary>
  /// Gets or sets the maximum number of tokens to use when splitting documents for embeddings.
  /// For more details on tokens and how to count them, see:
  /// https://help.openai.com/en/articles/4936856-what-are-tokens-and-how-to-count-them
  /// </summary>
  [Range(0, int.MaxValue)]
  public int DocumentChunkMaxTokens { get; set; } = 100;

  /// <summary>
  /// Gets or sets the number of tokens to overlap between text chunks
  /// </summary>
  [Range(0, int.MaxValue)]
  public int DocumentChunkOverlapCount { get; set; } = 100;
}