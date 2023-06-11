using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnify.Interfaces
{
    public interface IMessenger
    {
        void Send<TMessage>(TMessage message);

        void Subscribe<TMessage>(object subscriber, Action<object> action);

        void Unsubscribe<TMessage>(object subscriber);
    }
}
