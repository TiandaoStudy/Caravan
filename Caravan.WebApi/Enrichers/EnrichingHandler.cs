using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Finsa.Caravan.Mvc.Core.Enrichers
{
    public sealed class EnrichingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var enrichers = request.GetConfiguration().GetResponseEnrichers();

            return enrichers.Where(e => e.CanEnrich(response))
                .Aggregate(response, (resp, enricher) => enricher.Enrich(response));
        }
    }
}