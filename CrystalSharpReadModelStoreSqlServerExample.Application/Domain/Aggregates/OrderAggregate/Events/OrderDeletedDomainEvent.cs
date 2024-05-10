using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events
{
    public class OrderDeletedDomainEvent : DomainEvent
    {
        public string OrderCode { get; set; }

        public OrderDeletedDomainEvent(Guid streamId, string orderCode)
        {
            StreamId = streamId;
            OrderCode = orderCode;
        }

        [JsonConstructor]
        public OrderDeletedDomainEvent(Guid streamId,
            string orderCode,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            OrderCode = orderCode;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
