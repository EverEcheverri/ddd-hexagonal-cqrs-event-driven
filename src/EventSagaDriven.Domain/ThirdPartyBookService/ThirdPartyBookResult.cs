using System.Text.Json.Serialization;
namespace EventSagaDriven.Domain.ThirdPartyBookService;

public class ThirdPartyBookResult
{
    [JsonPropertyName("available")]
    public long Available { get; set; }

    [JsonPropertyName("number")]
    public long Number { get; set; }

    [JsonPropertyName("offset")]
    public long Offset { get; set; }

    [JsonPropertyName("books")]
    public Book[][] Books { get; set; }
}


public class Book
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("image")]
    public Uri Image { get; set; }

    [JsonPropertyName("genres")]
    public string[] Genres { get; set; }

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; }
}