using System;
using Newtonsoft.Json;
using CrystalSharp.Domain.Infrastructure;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events
{
    public class OrderPlacedDomainEvent : DomainEvent
    {
        public Guid CustomerId { get; set; }
        public string OrderCode { get; set; }
        public string Item { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderPlacedDomainEvent(Guid streamId, Guid customerId, string orderCode, string item, decimal totalAmount)
        {
            StreamId = streamId;
            CustomerId = customerId;
            OrderCode = orderCode;
            Item = item;
            TotalAmount = totalAmount;
        }

        [JsonConstructor]
        public OrderPlacedDomainEvent(Guid streamId,
            Guid customerId,
            string orderCode,
            string item,
            decimal totalAmount,
            int entityStatus,
            DateTime createdOn,
            DateTime? modifiedOn,
            long version)
        {
            StreamId = streamId;
            CustomerId = customerId;
            OrderCode = orderCode;
            Item = item;
            TotalAmount = totalAmount;
            EntityStatus = entityStatus;
            CreatedOn = createdOn;
            ModifiedOn = modifiedOn;
            Version = version;
        }
    }
}
