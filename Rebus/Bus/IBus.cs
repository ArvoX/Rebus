using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rebus.Messages;
using Rebus.Messages.Control;
using Rebus.Routing;
using Rebus.Subscriptions;

namespace Rebus.Bus
{
    public interface IBus : IDisposable
    {
        /// <summary>
        /// Sends the specified message to our own input queue address
        /// </summary>
        Task SendLocal(object commandMessage, Dictionary<string, string> optionalHeaders = null);
        
        /// <summary>
        /// Sends the specified message to a destination that is determined by calling <see cref="IRouter.GetDestinationAddress"/>
        /// </summary>
        Task Send(object commandMessage, Dictionary<string, string> optionalHeaders = null);
        
        /// <summary>
        /// Sends the specified reply message to a destination that is determined by looking up the <see cref="Headers.ReturnAddress"/> header of the message currently being handled.
        /// This method can only be called from within a message handler.
        /// </summary>
        Task Reply(object replyMessage, Dictionary<string, string> optionalHeaders = null);

        /// <summary>
        /// Publishes the specified message to the specified topic. Default behavior is to look up the addresses of those who subscribed to the given topic
        /// by calling <see cref="ISubscriptionStorage.GetSubscriberAddresses"/> but the transport may override this behavior if it has special capabilities.
        /// </summary>
        Task Publish(string topic, object eventMessage, Dictionary<string, string> optionalHeaders = null);
        
        /// <summary>
        /// Subscribes the current endpoint to the given topic. If the <see cref="ISubscriptionStorage"/> is centralized (determined by checking <see cref="ISubscriptionStorage.IsCentralized"/>),
        /// the subscription is registered immediately. If not, the owner of the given topic is checked (by calling <see cref="IRouter.GetOwnerAddress"/>), and a
        /// <see cref="SubscribeRequest"/> is sent to the owning endpoint).
        /// </summary>
        Task Subscribe(string topic);

        /// <summary>
        /// Unsubscribes the current endpoint from the given topic. If the <see cref="ISubscriptionStorage"/> is centralized (determined by checking <see cref="ISubscriptionStorage.IsCentralized"/>),
        /// the subscription is removed immediately. If not, the owner of the given topic is checked (by calling <see cref="IRouter.GetOwnerAddress"/>), and an
        /// <see cref="UnsubscribeRequest"/> is sent to the owning endpoint).
        /// </summary>
        Task Unsubscribe(string topic);
    }
}