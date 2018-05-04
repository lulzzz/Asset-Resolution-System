using ars.lib.Common.Interfaces;
using Autofac;
using Newtonsoft.Json;
using System.IO;

namespace ars.lib.Common.Utils
{
    public static class Container
    {
        public static IContainer Initialize()
        {
            var filePath = "AppConfig.json";
            var dbPath = "arsperstor.db3";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var jsonFile = File.ReadAllText(filePath);
            var mySettings = JsonConvert.DeserializeObject<AppSettings>(jsonFile);
            var myDb = new Ars_DAL(mySettings, dbPath);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(mySettings).As<ISettings>();
            builder.RegisterInstance(myDb).As<Ars_DAL>();
            return builder.Build();
        }
    }
}
