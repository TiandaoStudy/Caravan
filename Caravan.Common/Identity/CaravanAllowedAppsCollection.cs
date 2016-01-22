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

using System.Collections;
using System.Collections.Generic;

namespace Finsa.Caravan.Common.Identity
{
    /// <summary>
    ///   Rappresenta una collezione di applicativi Caravan che possono interagire con il servizio
    ///   di autenticazione e autorizzazione.
    /// </summary>
    public sealed class CaravanAllowedAppsCollection : ICollection<string>
    {
        private readonly HashSet<string> _allowedApps = new HashSet<string>();

        /// <summary>
        ///   Se impostato a vero, permette l'accesso da parte di tutti gli applicativi Caravan,
        ///   indipendentemente dal fatto che siano censiti tra quelli consentiti.
        /// </summary>
        public bool AllowAll { get; set; }

        public int Count => _allowedApps.Count;

        public bool IsReadOnly => AllowAll || ((ICollection<string>)_allowedApps).IsReadOnly;

        public void Add(string item)
        {
            if (AllowAll)
            {
                return;
            }
            _allowedApps.Add(item.ToLowerInvariant());
        }

        public void Clear()
        {
            if (AllowAll)
            {
                return;
            }
            _allowedApps.Clear();
        }

        public bool Contains(string item)
        {
            if (AllowAll)
            {
                return true;
            }
            return _allowedApps.Contains(item.ToLowerInvariant());
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            if (AllowAll)
            {
                // Tutte sono consentite...
                return;
            }
            _allowedApps.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            if (AllowAll)
            {
                yield break;
            }
            foreach (var allowedApp in _allowedApps)
            {
                yield return allowedApp;
            }
        }

        public bool Remove(string item)
        {
            if (AllowAll)
            {
                return false;
            }
            return _allowedApps.Remove(item.ToLowerInvariant());
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
