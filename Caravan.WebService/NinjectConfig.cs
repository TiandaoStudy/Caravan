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

using Finsa.Caravan.WebApi.Models.Identity;
using Finsa.CodeServices.Compression;
using Finsa.CodeServices.Security.PasswordHashing;
using Finsa.CodeServices.Serialization;
using Microsoft.AspNet.Identity;
using Ninject.Modules;
using PommaLabs.KVLite;

namespace Finsa.Caravan.WebService
{
    internal sealed class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ICache>().To<PersistentCache>().InSingletonScope().WithConstructorArgument("settings", PersistentCacheSettings.Default);
            Bind<ICompressor>().To<LZ4Compressor>().InSingletonScope();
            Bind<IPasswordHasher>().To<NoOpPasswordHasher>().InSingletonScope();
            Bind<ISerializer>().To<JsonSerializer>().WithConstructorArgument("settings", new JsonSerializerSettings());

            Bind<OAuth2AuthorizationSettings>().ToConstant(new OAuth2AuthorizationSettings
            {
                AccessTokenValidationUrl = "https://localhost/wsCaravan/identity/connect/accesstokenvalidation"
                
            });
        }
    }
}