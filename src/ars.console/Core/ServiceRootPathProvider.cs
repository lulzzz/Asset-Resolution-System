using Nancy;
using Nancy.Hosting.Self;
using System.IO;

namespace ars.console.Core
{
    internal class ServiceRootPathProvider : IRootPathProvider
    {
        private IRootPathProvider provider = new FileSystemRootPathProvider();
        public string GetRootPath()
        {
            return Path.GetFullPath(Path.Combine(provider.GetRootPath(), "..", ".."));
        }
    }
}
