namespace App.Oracle.Core.Web.API.Repository
{
    public interface IIniParser
    {
        string IniFilePath { get; }
        Dictionary<string, string?> GetAllKeysAndValues();
        Dictionary<string, string?> GetSectionKeysAndValues(string section);
        string? GetValue(string section, string sectionKey);
    }
}