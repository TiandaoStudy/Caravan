using Finsa.CodeServices.Common;
using System;

namespace Finsa.Caravan.Common.Models.Security
{
    /// <summary>
    ///   Rappresenta le modifiche che possono essere effettuate a una entità di
    ///   <see cref="SecUser"/>. Le due classi vanno tenute sincronizzate.
    /// </summary>
    [Serializable]
    public class SecUserUpdates
    {
        public Option<string> Login { get; set; }

        public Option<string> PasswordHash { get; set; }

        public Option<bool> Active { get; set; }

        public Option<string> FirstName { get; set; }

        public Option<string> LastName { get; set; }

        public Option<string> Email { get; set; }

        public Option<bool> EmailConfirmed { get; set; }

        public Option<string> PhoneNumber { get; set; }

        public Option<bool> PhoneNumberConfirmed { get; set; }
    }
}