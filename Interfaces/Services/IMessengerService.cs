using System;

namespace Magnify.Interfaces.Services
{
    public interface IMessengerService
    {
        void Send<TMessage>(TMessage message);

        void Subscribe<TMessage>(object subscriber, Action<object> action);

        void Unsubscribe<TMessage>(object subscriber);
    }
}
