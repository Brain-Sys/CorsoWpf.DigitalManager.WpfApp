using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.ViewModels
{
    public class ApplicationViewModelBase : ViewModelBase, IDisposable
    {
        // Gli altri viewmodel che ereditano da questo
        // possono modificare e specializzare il comportamento di Dispose()
        public virtual void Dispose()
        {
            
        }
    }
}