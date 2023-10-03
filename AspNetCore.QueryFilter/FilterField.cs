using System.Text.Json.Serialization;

namespace AspNetCore.QueryFilter;

public readonly record struct FilterField
{
    public FilterField(string key, Type type)
    {
        Key = key;
        Type = type;
        TypeName = type.FullName!;
        Values = new HashSet<object>();
    }

    public string Key { get; init; }
    public HashSet<object> Values { get; init; }

    [JsonIgnore]
    public Type Type { get; init; }

    [JsonPropertyName(nameof(Type))]
    public string TypeName { get; init; }

    public override int GetHashCode() => Key.GetHashCode();
}
