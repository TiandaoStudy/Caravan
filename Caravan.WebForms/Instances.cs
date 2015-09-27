using System;
using System.Configuration;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebForms.Core;
using Finsa.Caravan.WebForms.Properties;
using Finsa.CodeServices.Common.Reflection;
using FLEX.Web;
using ISecurityManager = FLEX.Web.ISecurityManager;
using Finsa.Caravan.Common;

namespace FLEX.WebForms
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
               CachedInstance = ServiceLocator.Load<IErrorManager>(Settings.Default.ErrorManagerTypeInfo);
            } catch (Exception ex) {
                CaravanServiceProvider.EmergencyLog.Fatal("Loading IErrorManager", ex);
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
               CachedInstance = ServiceLocator.Load<IPageManager>(Settings.Default.PageManagerTypeInfo);
            } catch (Exception ex) {
                CaravanServiceProvider.EmergencyLog.Fatal("Loading IPageManager", ex);
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
               CachedInstance = ServiceLocator.Load<ISecurityManager>(Settings.Default.SecurityManagerTypeInfo);
            } catch (Exception ex)
            {
                CaravanServiceProvider.EmergencyLog.Fatal("Loading ISecurityRepository", ex);
                throw new ConfigurationErrorsException(ErrorMessages.TopLevel_SecurityManager_ErrorLoadingType, ex);
            }
        }

        public static ISecurityManager Instance
        {
            get { return CachedInstance; }
        }
    }
}