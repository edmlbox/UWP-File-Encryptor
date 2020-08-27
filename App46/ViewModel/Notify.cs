using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App46.ViewModel
{
    public class Notify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        
        public void OnChange(string name)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
