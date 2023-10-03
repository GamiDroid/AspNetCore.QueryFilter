using System.Text.Json;

namespace AspNetCore.QueryFilter;

public interface IQueryFilter
{
    HashSet<FilterField> Filters { get; set; }

    void Add(KeyValuePair<string, object> keyValuePair);
}

public class QueryFilter : IQueryFilter
{
    public HashSet<FilterField> Filters { get; set; } = new();

    public void Add(KeyValuePair<string, object> keyValuePair)
    {
        var field = new FilterField(keyValuePair.Key, keyValuePair.Value.GetType()) { Values = new HashSet<object>() { keyValuePair.Value } };
        Filters.Add(field);
    }

    public static IQueryFilter FromJson(string json)
    {
        var tempQueryFilter = (IQueryFilter)JsonSerializer.Deserialize(json, typeof(QueryFilter))!;

        var resultQueryFilter = new QueryFilter();

        foreach (var field in tempQueryFilter.Filters)
        {
            var orignalType = Type.GetType(field.TypeName)!;
            var values = field.Values
                .Select(x => ((JsonElement)x).GetRawText())
                .Select(x => JsonSerializer.Deserialize(x, orignalType)!)
                .ToHashSet()!;

            resultQueryFilter.Filters.Add(new FilterField(field.Key, orignalType) { Values = values });
        }

        return resultQueryFilter;
    }
}
