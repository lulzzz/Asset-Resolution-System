using Nancy;

namespace ars.console.Routes
{
    public class IndexModule : NancyModule
    {

        public IndexModule()
        {
            Get["/"] = _ =>
            {
                return View["Index"];
            };
        }
    }
}
