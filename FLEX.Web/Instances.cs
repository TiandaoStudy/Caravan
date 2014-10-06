using System;
using System.Configuration;
using FLEX.Common.Data;
using PommaLabs.GRAMPA.Reflection;

namespace FLEX.Web.WebForms
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorManager
    {
        private static readonly IErrorManager CachedInstance;

        static ErrorManager()
        {
            try {
                CachedInstance = ServiceLocator.Load<IErrorManager>(Configuration.Instance.ErrorManagerTypeInfo);
            } catch (Exception ex) {
                DbLogger.Instance.LogFatal<IErrorManager>(ex, "Loading IErrorManager");
                throw new ConfigurationErrorsException(ErrorMessages.TopLevel_ErrorManager_ErrorLoadingType, ex);
            }
        }

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
        private static readonly IPageManager CachedInstance;

        static PageManager()
        {
            try {
                CachedInstance = ServiceLocator.Load<IPageManager>(Configuration.Instance.PageManagerTypeInfo);
            } catch (Exception ex) {
                DbLogger.Instance.LogFatal<IPageManager>(ex, "Loading IPageManager");
                throw new ConfigurationErrorsException(ErrorMessages.TopLevel_PageManager_ErrorLoadingType, ex);
            }
        }

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
        private static readonly ISecurityManager CachedInstance;

        static SecurityManager()
        {
            try {
                CachedInstance = ServiceLocator.Load<ISecurityManager>(Configuration.Instance.SecurityManagerTypeInfo);
            } catch (Exception ex) {
                DbLogger.Instance.LogFatal<ISecurityManager>(ex, "Loading ISecurityManager");
                throw new ConfigurationErrorsException(ErrorMessages.TopLevel_SecurityManager_ErrorLoadingType, ex);
            }
        }

        public static ISecurityManager Instance
        {
            get { return CachedInstance; }
        }
    }
}