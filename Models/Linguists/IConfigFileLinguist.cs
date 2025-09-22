using CSTG.ConfigVariable;

namespace CSTG.Models.Linguists;

public interface IConfigFileLinguist
{
    string MakeVariable(VariableTypes type, string value);
    Variable ReadVariable(string text);
    bool IsVariableType(string name);
}