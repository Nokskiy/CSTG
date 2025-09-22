namespace CSTG.ConfigVariable;

public struct Variable(VariableTypes type, string value)
{
    public VariableTypes Type => type;
    public string Value => value;
}