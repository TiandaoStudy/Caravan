﻿using System.IO;
using System.Web;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Core;
using FLEX.Common.XmlSchemas.MenuEntries;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class SecurityModule : CustomModule
   {
      public SecurityModule() : base("security")
      {
         Get["/menu"] = p =>
         {
            var xml = File.ReadAllText(HttpContext.Current.Server.MapPath("~/bin/MenuBar.xml"));
            using (var stream = new StringReader(xml))
            {
               Menu.DeserializeFrom(stream);
            }
            return xml;
         };

         Get["/apps"] = _ => Response.AsJson(Db.SecurityManager.Apps());
         Get["/apps/{appName}"] = p => Response.AsJson(Db.SecurityManager.App((string) p.appName));
         Get["/apps/{appName}/groups"] = p => Response.AsJson(Db.SecurityManager.Groups((string) p.appName));
         Get["/apps/{appName}/users"] = p => Response.AsJson(Db.SecurityManager.Users((string) p.appName));
      }
   }
}