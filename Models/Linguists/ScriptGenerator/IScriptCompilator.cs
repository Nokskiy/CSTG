namespace CSTG.Models.Linguists.ScriptGenerator;

public interface IScriptCompilator
{
    void Compilate(string inputFile);
    bool IsVariable(string text);
    Variable ConvertVariable(string text);
    bool IsVariableType(string name);
}