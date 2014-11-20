namespace Finsa.Caravan.Mvc.Controls.ValueHolders
{
   public sealed class JQueryAutoCompleteOptions
   {
      public JQueryAutoCompleteOptions()
      {
         // Default values
         AutoFocus = false;
         Delay = 300;
      }

      /// <summary>
      ///   If set to true the first item will automatically be focused when the menu is shown.
      /// </summary>
      public bool AutoFocus { get; set; }

      /// <summary>
      ///   The delay in milliseconds between when a keystroke occurs and when a search is performed. 
      ///   A zero-delay makes sense for local data (more responsive), but can produce a lot of load for remote data, 
      ///   while being less responsive.
      /// </summary>
      public int Delay { get; set; }
   }
}
