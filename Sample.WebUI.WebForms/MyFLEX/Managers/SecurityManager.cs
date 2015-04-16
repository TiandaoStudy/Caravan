using System.Security.Principal;
using FLEX.Web;

namespace FLEX.Sample.WebUI.MyFLEX.Managers
{
   public sealed class SecurityManager : ISecurityManager
   {
      public string ApplyMenuSecurity(IPrincipal user, string xmlToBeChecked)
      {
         return xmlToBeChecked;
      }
   }
}