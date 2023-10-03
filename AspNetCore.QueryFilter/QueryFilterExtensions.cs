using System.Text.Json;

namespace AspNetCore.QueryFilter;
public static class QueryFilterExtensions
{
    public static string GetQueryString(this IQueryFilter filter)
    {
        var asJson = JsonSerializer.Serialize(filter);
        return $"filter={asJson}";
    }
}
