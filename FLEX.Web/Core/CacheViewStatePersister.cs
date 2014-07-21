using System.Web.UI;

namespace FLEX.Web.Core
{
   /// <summary>
   /// This class is an example of a BaseStatePersister implementation.
   /// </summary>
   internal sealed class CacheViewStatePersister : BaseStatePersister
   {
      //required constructor
      public CacheViewStatePersister(Page page) : base(page)
      {
         ViewStateSettings = new ViewStateStorageSettings();
      }

      public override void Load()
      {
         var guid = Page.Request.Form[HiddenFieldName];

         // using the unique id, fetch the serialized viewstate data,
         // possibly from an internal method
         var state = GetViewState(guid);

         // the state object is a System.Web.UI.Pair, because we must set the ControlState as well
         var pair = state as Pair;
         if (pair != null)
         {
            ControlState = pair.First;
            ViewState = pair.Second;
         }
      }

      public override void Save()
      {
         var guid = GetViewStateId();
         Page.ClientScript.RegisterHiddenField(HiddenFieldName, guid);
         SetViewState(guid);
      }

      public override void Clear()
      {
         // seek and remove records that may have expired
      }

      private static object GetViewState(string guid)
      {
         object viewState;
         CacheManager.TryRetrieveValueForKey(out viewState, HiddenFieldName, guid);
         return viewState;
      }

      private void SetViewState(string guid)
      {
         object state = new Pair(ControlState, ViewState);
         CacheManager.StoreSlidingValueForKey(state, HiddenFieldName, guid);
      }
   }
}