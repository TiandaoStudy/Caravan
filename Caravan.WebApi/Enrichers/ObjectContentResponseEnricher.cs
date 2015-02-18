﻿using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Finsa.Caravan.Mvc.Core.Enrichers
{
    /// <summary>
    ///   A base class for response enrichers that require access to the response object content.
    /// </summary>
    public abstract class ObjectContentResponseEnricher<T> : IResponseEnricher
    {
        private HttpResponseMessage _httpResponse;
        private UrlHelper _urlHelper;

        public virtual bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T);
        }

        public abstract void Enrich(T content);

        bool IResponseEnricher.CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;
            return (content != null && CanEnrich(content.ObjectType));
        }

        HttpResponseMessage IResponseEnricher.Enrich(HttpResponseMessage response)
        {
            _httpResponse = response;
            _urlHelper = new UrlHelper(response.RequestMessage);

            T content;
            if (response.TryGetContentValue(out content))
            {
                Enrich(content);
            }

            return response;
        }

        protected HttpRequestMessage Request
        {
            get { return _httpResponse.RequestMessage; }
        }

        protected UrlHelper Url
        {
            get { return _urlHelper; }
        }
    }
}