using Magnify.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Model.Messages
{
    public class NavigationMessage
    {
        public BaseViewModel CurrentViewModel { get; }

        public NavigationMessage(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }
    }
}
