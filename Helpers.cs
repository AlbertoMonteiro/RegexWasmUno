using System.Text;
using System.Text.RegularExpressions;

namespace RegexWasmUno;

public static class Helpers
{
    public static string EvaluateRegex(string regex, string value)
    {
        StringBuilder stringBuilder = new StringBuilder();
        RunRegex(regex, value, 0, stringBuilder);
        return stringBuilder.ToString();
    }

    private static void RunRegex(string regex, string value, int regexOptions, StringBuilder builder)
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
}