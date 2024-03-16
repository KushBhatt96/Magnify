using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        public ObservableCollection<string> Names { get; } = new ObservableCollection<string>
        {
            "LeBron",
            "Kobe",
            "MJ",
            "Luka",
            "Giannis"
        };
    }
}
