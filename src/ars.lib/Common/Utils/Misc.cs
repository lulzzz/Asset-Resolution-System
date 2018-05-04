using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ars.lib.Common.Interfaces;
using System.IO;

namespace ars.lib.Common.Utils
{
    public static class Misc
    {
        public static void SaveSettings(this ISettings settings)
        {
            using (StreamWriter file = File.CreateText(settings.ConfigFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, settings);
            }
        }
    }
}
