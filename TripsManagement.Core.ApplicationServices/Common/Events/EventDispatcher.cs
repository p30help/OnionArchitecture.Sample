using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TripsManagement.Core.DomainModels.Common.Events;

namespace TripsManagement.Core.ApplicationServices.Common.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Event Dispatcher
        public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent
        {
            // get event handler instance
            var eventHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());

            // get handlers
            var handlers = _serviceProvider.GetServices(eventHandlerType);

            foreach (var handler in handlers)
            {
                try
                {
                    dynamic dynamicEvent = @event;

                    dynamic dynamicHandler = handler;
                    await dynamicHandler.Handle(dynamicEvent);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }

        public async Task PublishDomainEventsAsync<TDomainEvent>(IEnumerable<TDomainEvent> events) where TDomainEvent : class, IDomainEvent
        {
            foreach (var domainEvent in events)
            {
                await PublishDomainEventAsync(domainEvent);
            }

        }

        #endregion

    }

}
