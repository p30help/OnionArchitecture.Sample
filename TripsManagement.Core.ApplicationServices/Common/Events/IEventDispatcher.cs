using System.Collections.Generic;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Common.Events;

namespace TripsManagement.Core.ApplicationServices.Common.Events
{
    public interface IEventDispatcher
    {
        Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;

        Task PublishDomainEventsAsync<TDomainEvent>(IEnumerable<TDomainEvent> @events) where TDomainEvent : class, IDomainEvent;

    }
}
