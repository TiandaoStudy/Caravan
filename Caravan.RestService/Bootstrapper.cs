﻿using Finsa.Caravan.RestService.Core;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class Bootstrapper : CachingBootstrapper
   {
      // The bootstrapper enables you to reconfigure the composition of the framework,
      // by overriding the various methods and properties.
      // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

      protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
      {
         base.ApplicationStartup(container, pipelines);
         StaticConfiguration.DisableErrorTraces = false;
      }

      protected override void ConfigureApplicationContainer(TinyIoCContainer container)
      {
         base.ConfigureApplicationContainer(container);
         container.Register<JsonSerializer, CustomJsonSerializer>();
      }

      protected override void ConfigureConventions(NancyConventions nancyConventions)
      {
         nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/", @"Admin"));
         base.ConfigureConventions(nancyConventions);
      }
   }
}