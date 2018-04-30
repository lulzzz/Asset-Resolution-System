using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ars.lib.Common.Interfaces;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace ars.lib.Common.Utils
{
    public static class Container
    {
        public static IContainer Initialize()
        {
            var filePath = "AppConfig.json";

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var jsonFile = File.ReadAllText(filePath);
            var mySettings = JsonConvert.DeserializeObject<AppSettings>(jsonFile);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(mySettings).As<ISettings>();
            return builder.Build();
        }
    }
}
