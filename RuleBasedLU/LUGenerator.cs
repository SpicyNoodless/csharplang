namespace RuleBasedLU;

public interface ISemanticFrame
{
    public string Domain { get; set; }
    public string Intent { get; set; }

    public string ToString();
}

public class SemanticFrame : ISemanticFrame
{
    public string Domain { get; set; } = null!;
    public string Intent { get; set; } = null!;

    public override string ToString()
    {
        return $"({Domain}, {Intent})";
    }
}

public class LUGenerator
{
    public static SemanticFrame GenerateSemanticFrame(string query, string market)
    {
        var lu = Enum.GetValues(typeof(PlayMyEmailLU)).Cast<PlayMyEmailLU>().FirstOrDefault(i => i.IsMatch(query, market), PlayMyEmailLU.Fallback);
        var (domain, intent) = PlayMyEmailLUExtension.DomainAndIntent(lu);
        return new SemanticFrame { Domain = domain, Intent = intent };
    }
}
