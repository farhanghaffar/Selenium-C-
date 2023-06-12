using System.IO;
using System.Reflection;
using System.Text;


namespace MRO.ROI.Automation.Utility
{
    public class IniFile : KernelBaseType
    {
        private string path;

        public IniFile(string Path)
        {
            path = Path;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, string.Empty, temp, 255, this.path);
            return temp.ToString();
        }

        public class IniHelper
        {
            public static string ReadConfig(string section, string key)
            {
                string  iniFileNameAlongwithPath = $"{Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "TestData\\MRO.ini"))}";
                string retVal = string.Empty;
                string bankname = string.Empty;
                string basePath = Path.GetDirectoryName(iniFileNameAlongwithPath);
                IniFile ini = new IniFile(iniFileNameAlongwithPath);
                retVal = ini.IniReadValue(section, key);
                return retVal;

            }

            public static void WriteConfig(string section, string key, string value)
            {
                string iniFileNameAlongwithPath = $"{Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "TestData\\MRO.ini"))}";
                string retVal = string.Empty;
                string bankname = string.Empty;
                string basePath = Path.GetDirectoryName(iniFileNameAlongwithPath);

                IniFile ini = new IniFile(iniFileNameAlongwithPath);

                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                ini.IniWriteValue(section, key, value);
            }

        }

    }
}
