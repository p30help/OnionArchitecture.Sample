using System;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.DomainModels.Common
{
    public abstract class Entity
    {
        public BusinessId Id { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());

        protected Entity() { }
    }
}
