using Finsa.Caravan.Common.Security.Models;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;
using System;

namespace Finsa.Caravan.Common.Security
{
    public class CaravanUserManager : UserManager<SecUser, string>
    {
        public CaravanUserManager(IUserStore<SecUser, string> store, IPasswordHasher passwordHasher)
            : base(store)
        {
            Raise<ArgumentNullException>.IfIsNull(passwordHasher);
            PasswordHasher = passwordHasher;
        }

        public CaravanUserManager(ICaravanSecurityRepository securityRepository, IPasswordHasher passwordHasher)
            : this(new CaravanUserStore(securityRepository), passwordHasher)
        {
        }
    }
}
