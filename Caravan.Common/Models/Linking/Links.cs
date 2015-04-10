using System;
using System.Collections;
using System.Collections.Generic;
using Finsa.Caravan.Common.Utilities.Collections;
using Finsa.Caravan.Common.Utilities.Diagnostics;

namespace Finsa.Caravan.Common.Models.Linking
{
    public sealed class Links : IEnumerable<Link>
    {
        private readonly ThinLinkedList<Link> _links = new ThinLinkedList<Link>();

        public const string PropertyName = "_links";
        public const int PropertyOrder = 1000;

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