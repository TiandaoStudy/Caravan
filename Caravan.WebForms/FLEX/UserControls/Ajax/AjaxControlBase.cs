using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.DataAccess;
using System;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
    /// <summary>
    ///   TODO
    /// </summary>
    public abstract class AjaxControlBase : ControlBase
    {
        private static readonly ICaravanLog Log = CaravanServiceProvider.FetchLog<AjaxControlBase>();

        private const string DoPostBackViewStateKey = "AjaxControlBase.DoPostBack";
        private const string EnabledViewStateKey = "AjaxControlBase.Enabled";

        internal const bool DoPostBackDefaultValue = false;
        internal const bool EnabledDefaultValue = true;

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetDefaultValues();
                }
            }
            catch (Exception ex) when (Log.Rethrowing(ex))
            {
            }
        }

        #region Public Properties

        /// <summary>
        ///   TODO
        /// </summary>
        public bool? DoPostBack
        {
            get { return (bool?) ViewState[DoPostBackViewStateKey]; }
            set
            {
                var doPostBack = value ?? DoPostBackDefaultValue;
                ViewState[DoPostBackViewStateKey] = doPostBack;
                OnDoPostBackChanged(doPostBack);
            }
        }

        /// <summary>
        ///   TODO
        /// </summary>
        public bool? Enabled
        {
            get { return (bool?) ViewState[EnabledViewStateKey]; }
            set
            {
                var enabled = value ?? EnabledDefaultValue;
                ViewState[EnabledViewStateKey] = enabled;
                OnEnabledChanged(enabled);
            }
        }

        #endregion Public Properties

        protected virtual void SetDefaultValues()
        {
            DoPostBack = DoPostBack ?? DoPostBackDefaultValue;
            Enabled = Enabled ?? EnabledDefaultValue;
        }

        protected virtual void OnDoPostBackChanged(bool doPostBack)
        {
            // Empty...
        }

        protected virtual void OnEnabledChanged(bool enabled)
        {
            // Empty...
        }
    }
}
