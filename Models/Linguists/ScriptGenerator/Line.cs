using System.Text.RegularExpressions;

namespace CSTG.Models.Linguists.ScriptGenerator;

public class Line(string expression, string comment = null!)
{
    public string Expression => expression;
    public string? Comment => comment;

    public static string GetExpressionFromText(string text)
    {
        var pattern = @"^(.*?)(?:\s*//\s*.*)?$";
        return Regex.Match(text, pattern).Groups[1].Value.Trim();
    }

    public static string GetCommentFromText(string text)
    {
        var pattern = @"^(.*?)(?:\s*//\s*.*)?$";
        return Regex.Match(text, pattern).Groups[2].Value.Trim();
    }
}