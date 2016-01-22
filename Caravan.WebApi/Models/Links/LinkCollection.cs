// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Finsa.CodeServices.Common.Collections;
using PommaLabs.Thrower;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Finsa.Caravan.WebApi.Models.Links
{
    public sealed class LinkCollection : IEnumerable<Link>
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
