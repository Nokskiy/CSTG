namespace CSTG.Models.Linguists.ScriptGenerator;

public struct Variable(VariableTypes type, string name, string value)
{
    public VariableTypes Type => type;
    public string Name => name;
    public string Value => value;

    public override string ToString()
    {
        return $"{Name}={Value}";
    }
}