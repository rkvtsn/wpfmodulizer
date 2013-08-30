using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WpfModulizer.Library
{
    public class EventAggregator
    {
        EventAggregator() { }
        static EventAggregator() { }
        static readonly EventAggregator MInstance = new EventAggregator();

        private readonly IDictionary<string, IList> _subscriptions = new Dictionary<string, IList>();




        #region @Pool->Methods

        public void UnSubscribe<TMessage>(ISubscription<TMessage> subscription)
            where TMessage : EventMessage
        {
            if (_subscriptions.ContainsKey(subscription.EventName))
                _subscriptions[subscription.EventName].Remove(subscription);
        }

        public void ClearAllSubscriptions()
        {
            ClearAllSubscriptions(null);
        }

        public void ClearAllSubscriptions(string[] exceptMessages)
        {
            foreach (var messageSubscriptions in new Dictionary<string, IList>(_subscriptions))
            {
                bool canDelete = true;
                if (exceptMessages != null)
                    canDelete = !exceptMessages.Contains(messageSubscriptions.Key);

                if (canDelete)
                    _subscriptions.Remove(messageSubscriptions);
            }
        }

        #endregion @Pool->Methods


        static public bool IfSubscribed(string eventName)
        {
            return MInstance._subscriptions.ContainsKey(eventName);
        }

        static public ISubscription<EventMessage> Subscribe(string eventName, Action<EventMessage> action)
        {
            var subscription = new Subscription<EventMessage>(MInstance, action, eventName);

            if (MInstance._subscriptions.ContainsKey(eventName))
                MInstance._subscriptions[eventName].Add(subscription);
            else
                MInstance._subscriptions.Add(eventName, new List<ISubscription<EventMessage>> { subscription });

            return subscription;
        }

        static public void Publish(string eventName, EventMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");

            if (MInstance._subscriptions.ContainsKey(eventName))
            {
                var subscriptionList = new List<ISubscription<EventMessage>>(
                    MInstance._subscriptions[eventName].Cast<ISubscription<EventMessage>>());
                foreach (var subscription in subscriptionList)
                    subscription.Action(message);
            }
        }

        static public void Publish(string eventName, IDictionary<string, dynamic> data)
        {
            if (data == null || data.Count == 0) throw new ArgumentNullException("data");

            var em = new EventMessage { Messages = data };
            EventAggregator.Publish(eventName, em);
        }

        static public void Publish(string eventName, dynamic message)
        {
            if (message == null) throw new ArgumentNullException("message");

            var em = new EventMessage { Message = message };
            EventAggregator.Publish(eventName, em);
        }

        static public void UnSubscribe(ISubscription<EventMessage> subscription)
        {
            if (MInstance._subscriptions.ContainsKey(subscription.EventName))
                MInstance._subscriptions[subscription.EventName].Remove(subscription);
        }

    }


    #region @Message

    public class EventMessage
    {
        public EventMessage()
        {
            _dateTime = DateTime.Now;
            this.Messages = new Dictionary<string, dynamic>();
        }

        public dynamic Message { get; set; }

        public IDictionary<string, dynamic> Messages { get; set; }

        private readonly DateTime _dateTime;
        public DateTime DateTime { get { return _dateTime; } }

        public T MessageAs<T>() where T : new() { return (this.Message is T) ? this.Message : new T(); }
        static public T MessageAs<T>(dynamic message) where T : new() { return (message is T) ? message : new T(); }
    }

    #endregion @Message



    #region @Subscription

    public interface ISubscription<TMessage> : IDisposable where TMessage : EventMessage
    {
        String EventName { get; }
        Action<TMessage> Action { get; }
        //EventAggregator EventAggregator { get; }
    }

    public class Subscription<TMessage> : ISubscription<TMessage> where TMessage : EventMessage
    {
        public string EventName { get; private set; }
        public Action<TMessage> Action { get; private set; }
        public EventAggregator EventAggregator { get; private set; }

        public Subscription(EventAggregator eventAggregator, Action<TMessage> action, string eventName)
        //public Subscription(Action<TMessage> action, string eventName)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (action == null) throw new ArgumentNullException("action");
            if (eventName == null || eventName.Trim() == string.Empty) throw new ArgumentNullException("eventName");

            EventName = eventName;
            EventAggregator = eventAggregator;
            Action = action;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.EventAggregator.UnSubscribe(this);
        }
    }

    #endregion @Subscription
}
