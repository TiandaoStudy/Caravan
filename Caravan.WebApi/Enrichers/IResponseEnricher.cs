using System.Net.Http;

namespace Finsa.Caravan.Mvc.Core.Enrichers
{
    public interface IResponseEnricher
    {
        bool CanEnrich(HttpResponseMessage response);

        HttpResponseMessage Enrich(HttpResponseMessage response);
    }
}