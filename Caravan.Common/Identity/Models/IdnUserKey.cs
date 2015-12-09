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

using PommaLabs.Thrower;

namespace Finsa.Caravan.Common.Identity.Models
{
    /// <summary>
    ///   Rappresenta la chiave di un utente di Caravan. Viene usata nei subject dei token
    ///   rilasciati da OAuth2 per identificare l'utente relativo al token.
    /// </summary>
    public struct IdnUserKey
    {
        /// <summary>
        ///   Il carattere usato per separare l'identificativo dell'utente dal nome dell'applicativo
        ///   Caravan all'interno della stringa destinata al subject.
        /// </summary>
        const char Separator = '@';

        /// <summary>
        ///   Il nome dell'applicativo Caravan.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        ///   Identificativo sequenziale dell'utente.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///   Converte l'informazione in stringa, al fine di usarla come subject per il token.
        /// </summary>
        /// <returns>
        ///   L'informazione di chiave convertita in una stringa usabile come subject per i token
        ///   rilasciati da OAuth2.
        /// </returns>
        public override string ToString() => ToString(AppName, UserId);

        /// <summary>
        ///   Converte l'informazione in stringa, al fine di usarla come subject per il token.
        /// </summary>
        /// <param name="appName">Il nome dell'applicativo Caravan.</param>
        /// <param name="userId">Identificativo sequenziale dell'utente.</param>
        /// <returns>
        ///   L'informazione di chiave convertita in una stringa usabile come subject per i token
        ///   rilasciati da OAuth2.
        /// </returns>
        public static string ToString(string appName, long userId)
        {
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), "Caravan application name must be specified");
            return $"{userId}{Separator}{appName}";
        }

        /// <summary>
        ///   Recupera le informazioni sulla chiave dell'utente dalla stringa di subject.
        /// </summary>
        /// <param name="subject">La stringa di subject trovata nel token.</param>
        /// <returns>Le informazioni sulla chiave dell'utente.</returns>
        public static IdnUserKey FromString(string subject)
        {
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(subject), "Subject string is null, empty or blank");

            var separatorIndex = subject.IndexOf(Separator);
            var validSeparatorIndex = (separatorIndex > 0) && (separatorIndex < subject.Length - 1);
            RaiseArgumentException.IfNot(validSeparatorIndex, $"Separator was not found in the subject string or it has an invalid position: {subject}");

            long userId;
            var userIdPart = subject.Substring(0, separatorIndex);
            RaiseArgumentException.IfNot(long.TryParse(userIdPart, out userId), $"User ID has not the correct format: {userIdPart}");

            ++separatorIndex;
            var appName = subject.Substring(separatorIndex, subject.Length - separatorIndex);

            return new IdnUserKey
            {
                AppName = appName,
                UserId = userId
            };
        }
    }
}
