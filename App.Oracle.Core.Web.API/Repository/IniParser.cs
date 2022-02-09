namespace App.Oracle.Core.Web.API.Repository
{
    /// <summary>
    /// IniParser class.
    /// </summary>
    public class IniParser : IIniParser
    {
        /// <summary>
        /// Section key pair/value dictionary.
        /// </summary>
        private readonly Dictionary<SectionKeyPair, string> keyPairs;

        /// <summary>
        /// Initializes a new instance of the <see cref="IniParser"/> class.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown when the provided ini file path doesnt exist.</exception>
        public IniParser(string iniFilePath)
        {
            if (!File.Exists(iniFilePath))
            {
                throw new FileNotFoundException(nameof(iniFilePath));
            }

            keyPairs = new Dictionary<SectionKeyPair, string>();
            IniFilePath = iniFilePath;
            EnumerateSectionKeyPairs();
        }


        /// <summary>
        /// Gets the source ini file full path.
        /// </summary>
        public string IniFilePath { get; }


        /// <summary>
        /// Returns the value for the given section key. Returns null, if key doesn't exist.
        /// </summary>
        public string? GetValue(string section, string sectionKey)
        {
            var skp = new SectionKeyPair(section, sectionKey);

            if (keyPairs.TryGetValue(skp, out string keyValue))
            {
                return keyValue;
            }

            return null;
        }

        /// <summary>
        /// Gets keys and their values for given section.
        /// </summary>
        public Dictionary<string, string?> GetSectionKeysAndValues(string section)
        {
            var keysAndValues = new Dictionary<string, string?>();

            foreach (var skp in keyPairs.Keys)
            {
                if (skp.Section.Equals(section))
                {
                    string? keyValue = GetValue(section, skp.SectionKey);
                    keysAndValues.Add(skp.SectionKey, keyValue);
                }
            }

            return keysAndValues;
        }

        /// <summary>
        /// Gets all keys and their values.
        /// </summary>
        public Dictionary<string, string?> GetAllKeysAndValues()
        {
            var keysAndValues = new Dictionary<string, string?>();

            foreach (var skp in keyPairs.Keys)
            {
                string? keyValue = GetValue(skp.Section, skp.SectionKey);
                keysAndValues.Add(skp.SectionKey, keyValue);
            }

            return keysAndValues;
        }


        /// <summary>
        /// Reads the ini file and enumerates its values.
        /// </summary>
        /// <exception cref="IniFileEmptyException">Thrown when the ini file is empty.</exception>
        /// <exception cref="IniEnumerationFailedException">Thrown when the ini key/value pair enumeration failed.</exception>
        private void EnumerateSectionKeyPairs()
        {
            keyPairs.Clear();

            string[] iniFile = File.ReadAllLines(IniFilePath, Encoding.UTF8);

            if (iniFile.Length == 0)
            {
                throw new Exception($"'{IniFilePath}' file is empty");
            }

            string currentSection = string.Empty;

            foreach (var line in iniFile)
            {
                string currentLine = line;

                // check for ';' or '#' chars, and ignore the rest of the string (comments)
                int pos = currentLine.IndexOfAny(new char[] { ';', '#' });

                if (pos >= 0)
                {
                    string[] split = currentLine.Split(currentLine[pos]);
                    currentLine = split[0];
                }

                currentLine = currentLine.Trim();

                if (currentLine != string.Empty)
                {
                    if (currentLine.StartsWith("[") && currentLine.EndsWith("]"))
                    {
                        currentSection = currentLine[1..^1];
                    }
                    else
                    {
                        string[] keyPair = currentLine.Split(new char[] { '=' }, 2);

                        if (keyPair.Length != 2)
                        {
                            throw new Exception("Ini key/value pair enumeration failed");
                        }

                        var skp = new SectionKeyPair(currentSection, keyPair[0].Trim());
                        keyPairs.Add(skp, keyPair[1].Trim());
                    }
                }
            }
        }


        /// <summary>
        /// SectionKeyPair struct.
        /// </summary>
        private struct SectionKeyPair
        {
            public readonly string Section;
            public readonly string SectionKey;

            public SectionKeyPair(string section, string sectionKey)
            {
                Section = section;
                SectionKey = sectionKey;
            }
        }
    }
}
