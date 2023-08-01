using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Services.Common;

/// <summary>
/// JSON 返回日期格式格式化
/// </summary>
public class DateTimeConverter : JsonConverter<DateTime>
{
    public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateTime.Parse(reader.GetString());

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(this.DateTimeFormat));
}