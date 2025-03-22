using Magnify.Interfaces.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Magnify.Services
{
    public class MessengerService : IMessengerService
    {
        private static IMessengerService? _instance;
        private static readonly object _lock = new object();

        private readonly ConcurrentDictionary<Type, List<Subscription>> _subscriptions;

        private readonly ConcurrentDictionary<Type, object> _currentState;

        private MessengerService()
        {
            _subscriptions = new ConcurrentDictionary<Type, List<Subscription>>();
            _currentState = new ConcurrentDictionary<Type, object>();
        }

        public static IMessengerService Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = new MessengerService();
                    }
                }
                return _instance;
            }
        }

        #region Public Instance Methods

        public void Send<TMessage>(TMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            if (_subscriptions.ContainsKey(typeof(TMessage)))
            {
                var subscriptionsForMessageType = _subscriptions[typeof(TMessage)];

                if (_currentState.ContainsKey(typeof(TMessage)))
                {
                    _currentState[typeof(TMessage)] = message;
                }
                else
                {
                    _currentState.TryAdd(typeof(TMessage), message);
                }

                foreach (var subscription in subscriptionsForMessageType)
                {
                    subscription.Action(message);
                }
            }
        }

        public void Subscribe<TMessage>(object subscriber, Action<object> action)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var newSubscription = new Subscription(subscriber, action);

            if (!_subscriptions.ContainsKey(typeof(TMessage)))
            {
                var newSubscriptionList = new List<Subscription> { newSubscription };
                _subscriptions.TryAdd(typeof(TMessage), newSubscriptionList);
            }
            else
            {
                _subscriptions[typeof(TMessage)].Add(newSubscription);
                if (_currentState.ContainsKey(typeof(TMessage)))
                {
                    var latestMessageState = _currentState[typeof(TMessage)];
                    action(latestMessageState);
                }
            }


        }

        public void Unsubscribe<TMessage>(object subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            if (_subscriptions.ContainsKey(typeof(TMessage)))
            {
                var subscriptionsForMessageType = _subscriptions[typeof(TMessage)];

                var subscriptionToRemove = subscriptionsForMessageType.FirstOrDefault(subscription => subscription.Subscriber == subscriber);

                if (subscriptionToRemove != null)
                {
                    _subscriptions[typeof(TMessage)].Remove(subscriptionToRemove);
                }
            }
        }

        #endregion
    }

    public class Subscription
    {
        public object Subscriber { get; }
        public Action<object> Action { get; }

        public Subscription(object subscriber, Action<object> action)
        {
            Subscriber = subscriber;
            Action = action;
        }
    }
}
