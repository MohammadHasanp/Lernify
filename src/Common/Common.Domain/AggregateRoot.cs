namespace Common.Domain;

using System.ComponentModel.DataAnnotations.Schema;

public class AggregateRoot : Entity
{
    private readonly List<DomainEvent> baseDomainEvent = [];

    [NotMapped]
    public List<DomainEvent> BaseDomainEvent => this.baseDomainEvent;

    public void AddDomainEvent(DomainEvent baseDomain) => this.baseDomainEvent.Add(baseDomain);

    public void RemoveDomainEvent(DomainEvent baseDomain) => this.baseDomainEvent.Remove(baseDomain);
}
