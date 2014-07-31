using System.Security.Principal;

namespace FLEX.Common.Web
{
   public interface ISecurityManager
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="user"></param>
      /// <param name="xmlToBeChecked"></param>
      /// <returns></returns>
      string ApplyMenuSecurity(IPrincipal user, string xmlToBeChecked);
   }
}
