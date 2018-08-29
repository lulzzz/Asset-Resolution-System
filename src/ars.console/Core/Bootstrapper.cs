using Nancy.Conventions;
using System;
using Nancy;
using Nancy.Json;
using Nancy.Diagnostics;

namespace ars.console.Core
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {
#if !DEBUG
                 return base.DiagnosticsConfiguration;
#endif
#if DEBUG
                return new Nancy.Diagnostics.DiagnosticsConfiguration { Password = "password" };
#endif
            }
        }

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            //pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
            //    container.Resolve<IUserValidator>(),
            //    Environment.MachineName, UserPromptBehaviour.Never));

#if DEBUG
            StaticConfiguration.EnableRequestTracing = true;
            StaticConfiguration.Caching.EnableRuntimeViewDiscovery = true;
            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
#endif
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            //nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", "fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", "Scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Views", "Views"));

            JsonSettings.MaxJsonLength = Int32.MaxValue;
        }


        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new ServiceRootPathProvider();
            }
        }
    }
}
