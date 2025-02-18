namespace ErrorOr.Concept;

public class AbbreviationsOutput
{

    public List<Abbreviation> Abbreviations { get; set; } = new();

}

public class Abbreviation
{

    public string OriginalSentence { get; set; } = null!;

    public List<string> AbbreviationList { get; set; } = new();

}
