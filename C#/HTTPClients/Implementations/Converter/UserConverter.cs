using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Account.Models;
using Domain.Shopping.Models;

namespace HTTPClients.Implementations.Converter;

public class UserConverter : JsonConverter<User>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(User).IsAssignableFrom(typeToConvert);
    }

    public override User? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;
            if (root.TryGetProperty("IsSeller", out JsonElement isSellerElement) && isSellerElement.GetBoolean())
            {
                return JsonSerializer.Deserialize<Seller>(root.GetRawText(), options);
            }
            else
            {
                return JsonSerializer.Deserialize<User>(root.GetRawText(), options);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, User value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
