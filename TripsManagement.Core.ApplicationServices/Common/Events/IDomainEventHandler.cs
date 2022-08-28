using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Common.Events;

namespace TripsManagement.Core.ApplicationServices.Common.Events
{
    public interface IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        Task Handle(TDomainEvent Event);
    }

}
