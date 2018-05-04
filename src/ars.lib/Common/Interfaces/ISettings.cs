using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ars.lib.Common.Interfaces
{
    public interface ISettings
    {
        string AppName { get; set; }
        string AppDesc { get; set; }
        string AppServiceName { get; set; }
        string AppDbPwd { get; set; }
        string ConfigFile { get; set; }
        bool UseLogFile { get; set; }
    }
}
