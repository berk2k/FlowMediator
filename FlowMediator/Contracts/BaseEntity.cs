﻿namespace FlowMediator.Contracts
{
    /// <summary>
    /// Base class for domain entities that can raise domain events.
    /// </summary>
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
