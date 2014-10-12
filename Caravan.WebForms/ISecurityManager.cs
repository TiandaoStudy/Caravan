using System.Security.Principal;

namespace FLEX.Web
{
   /// <summary>
   /// 
   /// </summary>
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
