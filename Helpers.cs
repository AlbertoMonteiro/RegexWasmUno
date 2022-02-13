using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexWasmUno;

public static class Helpers
{
    private const string TRUE = "true";
    private const string FALSE = "false";

    public static void EvaluateRegex(string regex, string value)
    {
        Task.Run(() =>
        {
            StringBuilder builder = new StringBuilder();
            var buildSp = System.Diagnostics.Stopwatch.StartNew();
            var results = Regex.Matches(value, regex, RegexOptions.Multiline);
            builder.Append("{ matches: [");
            int i = 0;
            var result = results[i];
            builder.Append($"{{ success: {(result.Success ? TRUE : FALSE)}, groups: [");
            var regexGroups = result.Groups;
            int j = 0;
            var currentGroup = regexGroups[j];
            builder.Append($"{{index:{currentGroup.Index},length:{currentGroup.Length},Success:{(currentGroup.Success ? TRUE : FALSE)},name:{currentGroup.Name},value:\"{currentGroup.Value}\"}}");
            for (j++; j < regexGroups.Count; j++)
            {
                currentGroup = regexGroups[j];
                builder.Append($",{{index:{currentGroup.Index},length:{currentGroup.Length},Success:{(currentGroup.Success ? TRUE : FALSE)},name:{currentGroup.Name},value:\"{currentGroup.Value}\"}}");
            }
            builder.Append("]}");

            for (i++; i < results.Count; i++)
            {
                result = results[i];
                builder.Append($",{{ success: {(result.Success ? TRUE : FALSE)}, groups: [");
                regexGroups = result.Groups;
                j = 0;
                currentGroup = regexGroups[j];
                builder.Append($"{{index:{currentGroup.Index},length:{currentGroup.Length},Success:{(currentGroup.Success ? TRUE : FALSE)},name:{currentGroup.Name},value:\"{currentGroup.Value}\"}}");
                for (j++; j < regexGroups.Count; j++)
                {
                    currentGroup = regexGroups[j];
                    builder.Append($",{{index:{currentGroup.Index},length:{currentGroup.Length},Success:{(currentGroup.Success ? TRUE : FALSE)},name:{currentGroup.Name},value:\"{currentGroup.Value}\"}}");
                }
                builder.Append("]}");
            }
            buildSp.Stop();
            builder.Append($"], elapsed: {buildSp.ElapsedMilliseconds} }}");
            return Uno.Foundation.WebAssemblyRuntime.InvokeJS($"regexCallback({builder.ToString()})");
        });
    }
}