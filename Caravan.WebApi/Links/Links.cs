using System;
using System.Collections;
using System.Collections.Generic;
using PommaLabs.Collections;
using PommaLabs.Diagnostics;

namespace Finsa.Caravan.Mvc.Core.Links
{
    public sealed class Links : IEnumerable<Link>
    {
        private readonly ThinLinkedList<Link> _links = new ThinLinkedList<Link>();

        public const string JsonPropertyName = "$links";
        public const int JsonPropertyOrder = 1000;

        public void AddLink(Link link)
        {
            Raise<ArgumentNullException>.IfIsNull(link);
            _links.Add(link);
        }

        public void AddLinks(params Link[] links)
        {
            Raise<ArgumentNullException>.IfIsNull(links);
            foreach (var link in links)
            {
                AddLink(link);
            }
        }

        public IEnumerator<Link> GetEnumerator()
        {
            return _links.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _links.GetEnumerator();
        }
    }
}