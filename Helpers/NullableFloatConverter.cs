using System;
using Newtonsoft.Json;

public class NullableFloatConverter : JsonConverter<float?>
{
	public override float? ReadJson(JsonReader reader, Type objectType, float? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
			return null;
		if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
			return Convert.ToSingle(reader.Value);
		throw new JsonSerializationException($"Unexpected token type '{reader.TokenType}' when deserializing float.");
	}

	public override void WriteJson(JsonWriter writer, float? value, JsonSerializer serializer)
	{
		if (value.HasValue)
			writer.WriteValue(value.Value);
		else
			writer.WriteNull();
	}
}
