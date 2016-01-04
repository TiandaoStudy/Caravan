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

using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataAccess.Sql.Effort;
using Ninject;
using NUnit.Framework;

namespace Finsa.Caravan.UnitTests
{
    [TestFixture]
    internal abstract class AbstractTests
    {
        protected const string EmptyString = "";
        protected const string ShortString = "BOOM BABY";
        protected const string MediumString = "Reality continues to ruin my life.";
        protected const string LongString = @"If a man does not keep pace with his companions, perhaps it is because
                                              he hears a different drummer. Let him step to the music which he hears,
                                              however measured or far away.";

        protected const string TestAppName = "my-test-app";
        protected const string TestAppDescription = "My TEST App";
        protected const string TestUserLogin1 = "user1";

        protected ICaravanSecurityRepository SecurityRepository;
        protected ICaravanUserManagerFactory UserManagerFactory;

        static AbstractTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            CaravanServiceProvider.NinjectKernel = new StandardKernel(
                new CaravanCommonNinjectConfig(DependencyHandling.UnitTesting, "caravan"),
                new CaravanEffortDataAccessNinjectConfig(DependencyHandling.UnitTesting)
            );
        }

        [SetUp]
        public virtual void SetUp()
        {
            // Pulizia della sorgente dati.
            CaravanDataSource.Reset();

            // Ricarico le dipendenze necessarie.
            var kernel = CaravanServiceProvider.NinjectKernel;
            SecurityRepository = kernel.Get<ICaravanSecurityRepository>();
            UserManagerFactory = kernel.Get<ICaravanUserManagerFactory>();
        }

        [TearDown]
        public virtual void TearDown()
        {
            // Faccio pulizia all'interno delle dipendenze.
            UserManagerFactory = null;
            SecurityRepository?.Dispose();
            SecurityRepository = null;
        }
    }
}