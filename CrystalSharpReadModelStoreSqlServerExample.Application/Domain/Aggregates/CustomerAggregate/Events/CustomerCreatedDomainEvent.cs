using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.CustomerAggregate.Events
{
    public class CustomerCreatedDomainEvent : DomainEvent
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public CustomerCreatedDomainEvent(Guid streamId, string name, string address)
        {
            StreamId = streamId;
            Name = name;
            Address = address;
        }

        [JsonConstructor]
        public CustomerCreatedDomainEvent(Guid streamId,
            Guid customerId,
            string name,
            string address,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            Name = name;
            Address = address;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
