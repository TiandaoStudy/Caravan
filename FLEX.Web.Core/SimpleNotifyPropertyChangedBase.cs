using System.ComponentModel;
using System.Dynamic;

namespace FLEX.Web.Core
{
    public class SimpleNotifyPropertyChangedBase : INotifyPropertyChanged 
    { 
        public event PropertyChangedEventHandler PropertyChanged = (s, a) => { }; 

        protected dynamic Storage { get; private set; } 

        
        protected SimpleNotifyPropertyChangedBase()        
        {            
            Storage = new ExpandoObject();            
            var asNotificable = (INotifyPropertyChanged) Storage;            
            asNotificable.PropertyChanged += Notify;        
        }        
        
        protected void Notify(object sender, PropertyChangedEventArgs args) 
        { 
            PropertyChanged(sender, args); 
        } 
    }
}
