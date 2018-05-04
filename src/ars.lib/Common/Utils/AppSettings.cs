
namespace ars.lib.Common.Utils
{
    using Newtonsoft.Json;
    using System.IO;
    public class AppSettings : Interfaces.ISettings
    {
        public string AppName { get; set; }
        public string AppDesc { get; set; }
        public string AppServiceName { get; set; }
        public string AppDbPwd { get; set; }
        public string ConfigFile { get; set; }
        public bool UseLogFile { get; set; }
    }
}
