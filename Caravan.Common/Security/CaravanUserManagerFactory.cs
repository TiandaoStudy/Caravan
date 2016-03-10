﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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

using Finsa.CodeServices.MailSender;
using Microsoft.AspNet.Identity;
using Ninject;
using PommaLabs.Thrower;
using System;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Gestisce la creazione di UserManager specifici per un dato applicativo Caravan.
    /// </summary>
    sealed class CaravanUserManagerFactory : ICaravanUserManagerFactory
    {
        public CaravanUserManagerFactory(ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            SecurityRepository = securityRepository;
        }

        public ICaravanSecurityRepository SecurityRepository { get; }

        public Task<CaravanUserManager> CreateAsync() => CreateAsync(CaravanCommonConfiguration.Instance.AppName);

        public async Task<CaravanUserManager> CreateAsync(string appName)
        {
            var app = await SecurityRepository.GetAppAsync(appName);
            var userStore = new CaravanUserStore(app.Id, app.Name, SecurityRepository);
            var passwordHasherType = Type.GetType(app.PasswordHasher, true, true);
            var passwordHasher = CaravanServiceProvider.NinjectKernel.Get(passwordHasherType) as IPasswordHasher;
            var mailSender = CaravanServiceProvider.NinjectKernel.Get<IMailSender>();
            return new CaravanUserManager(userStore, passwordHasher, mailSender);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    SecurityRepository?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free
        //       unmanaged resources. ~CaravanUserManagerFactory() { // Do not change this code. Put
        // cleanup code in Dispose(bool disposing) above. Dispose(false); }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}
