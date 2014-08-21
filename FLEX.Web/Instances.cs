using FLEX.Common;

namespace FLEX.Web
{
   /// <summary>
   /// 
   /// </summary>
   public static class ErrorManager
   {
      private static readonly IErrorManager CachedInstance = ServiceLocator.Load<IErrorManager>(Configuration.Instance.ErrorManagerTypeInfo);

      public static IErrorManager Instance
      {
         get { return CachedInstance; }
      }
   }

   /// <summary>
   /// 
   /// </summary>
   public static class PageManager
   {
      private static readonly IPageManager CachedInstance = ServiceLocator.Load<IPageManager>(Configuration.Instance.PageManagerTypeInfo);

      public static IPageManager Instance
      {
         get { return CachedInstance; }
      }
   }

   /// <summary>
   /// 
   /// </summary>
   public static class SecurityManager
   {
      private static readonly ISecurityManager CachedInstance = ServiceLocator.Load<ISecurityManager>(Configuration.Instance.SecurityManagerTypeInfo);

      public static ISecurityManager Instance
      {
         get { return CachedInstance; }
      }
   }
}