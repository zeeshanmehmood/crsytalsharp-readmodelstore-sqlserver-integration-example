using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events
{
    public class OrderTotalAmountChangedDomainEvent : DomainEvent
    {
        public decimal TotalAmount { get; set; }

        public OrderTotalAmountChangedDomainEvent(Guid streamId, decimal totalAmount)
        {
            StreamId = streamId;
            TotalAmount = totalAmount;
        }

        [JsonConstructor]
        public OrderTotalAmountChangedDomainEvent(Guid streamId,
            decimal totalAmount,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            TotalAmount = totalAmount;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
