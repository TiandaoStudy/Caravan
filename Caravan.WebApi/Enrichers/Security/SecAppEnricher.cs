using Finsa.Caravan.Mvc.Core.Links;
using Finsa.Caravan.Mvc.Core.Models.Security;

namespace Finsa.Caravan.Mvc.Core.Enrichers.Security
{
    public sealed class SecAppEnricher : ObjectContentResponseEnricher<LinkedSecApp>
    {
        public override void Enrich(LinkedSecApp app)
        {
            app.Links.AddLink(new SelfLink(Url.Link("GetApps", new {})));
        }
    }
}