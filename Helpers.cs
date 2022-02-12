using System.Text;
using System.Text.RegularExpressions;

namespace RegexWasmUno;

public static class Helpers
{
    public static string EvaluateRegex(string regex, string value)
    {
        StringBuilder stringBuilder = new StringBuilder();
        SayHelloCS(regex, value, 0, stringBuilder);
        return stringBuilder.ToString();
    }

    private static void SayHelloCS(string regex, string value, int regexOptions, StringBuilder builder)
    {
        var sp = System.Diagnostics.Stopwatch.StartNew();
        var results = Regex.Matches(value, regex);
        builder.Append("{ \"matches\": [");
        for (int i = 0; i < results.Count; i++)
        {
            var result = results[i];
            if (i > 0)
                builder.Append(",");
            builder.Append($"{{ \"success\": {(result.Success ? "true" : "false")}, \"groups\": [");
            var regexGroups = result.Groups;
            for (int j = 0; j < regexGroups.Count; j++)
            {
                var currentGroup = regexGroups[j];
                if (j > 0)
                    builder.Append(",");
                builder.Append($"{{\"Index\":{currentGroup.Index},\"Length\":{currentGroup.Length},\"Success\":{(currentGroup.Success ? "true" : "false")},\"Name\":{currentGroup.Name},\"Value\":\"{currentGroup.Value}\"}}");
            }
            builder.Append("]}");
        }
        sp.Stop();
        builder.Append($"], \"elapsed\": {sp.ElapsedMilliseconds} }}");
    }

    public static string EvaluateRegexFull(string regex, string value)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var results = Regex.Matches(value, regex);
        var allResults = new Result[results.Count];
        for (int i = 0; i < allResults.Length; i++)
        {
            var result = results[i];
            var regexGroups = result.Groups;
            var groups = new RegexGroup[regexGroups.Count];
            for (int j = 0; j < groups.Length; j++)
            {
                var currentGroup = regexGroups[j];
                groups[j] = new RegexGroup(currentGroup.Index, currentGroup.Length, currentGroup.Success, currentGroup.Name, currentGroup.Value);
            }
            allResults[i] = new Result(result.Success, groups);
        }

        stopwatch.Stop();
        return System.Text.Json.JsonSerializer.Serialize(new AllResults(allResults, stopwatch.ElapsedMilliseconds));
    }
}

public record RegexGroup(int Index, int Length, bool Success, string Name, string Value);
public record Result(bool Success, RegexGroup[] Groups);
public record AllResults(Result[] allResults, double Elapsed);