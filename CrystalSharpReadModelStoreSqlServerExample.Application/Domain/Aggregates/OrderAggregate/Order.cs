using System;
using CrystalSharp.Common.Utilities;
using CrystalSharp.Domain;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate
{
    public class Order : AggregateRoot<int>
    {
        public Guid CustomerId { get; private set; }
        public string OrderCode { get; private set; }
        public string Item { get; private set; }
        public decimal TotalAmount { get; private set; }

        private static void ValidateOrder(Order order)
        {
            if (order.CustomerId == Guid.Empty)
            {
                order.ThrowDomainException("Customer ID is required.");
            }

            if (string.IsNullOrEmpty(order.OrderCode))
            {
                order.ThrowDomainException("Order code is required.");
            }

            if (string.IsNullOrEmpty(order.Item))
            {
                order.ThrowDomainException("Item is required.");
            }

            if (order.TotalAmount < 1)
            {
                order.ThrowDomainException("Invalid total amount.");
            }
        }

        public static Order PlaceOrder(Guid customerId, string item, decimal totalAmount)
        {
            Order order = new()
            {
                CustomerId = customerId,
                OrderCode = RandomGenerator.GenerateString(7),
                Item = item,
                TotalAmount = totalAmount
            };

            ValidateOrder(order);

            order.Raise(new OrderPlacedDomainEvent(order.GlobalUId, order.CustomerId, order.OrderCode, order.Item, order.TotalAmount));

            return order;
        }

        public void ChangeTotalAmount(decimal totalAmount)
        {
            TotalAmount = totalAmount;

            Raise(new OrderTotalAmountChangedDomainEvent(GlobalUId, TotalAmount));
        }

        public override void Delete()
        {
            base.Delete();
            Raise(new OrderDeletedDomainEvent(GlobalUId, OrderCode));
        }

        private void Apply(OrderPlacedDomainEvent @event)
        {
            CustomerId = @event.CustomerId;
            OrderCode = @event.OrderCode;
            Item = @event.Item;
            TotalAmount = @event.TotalAmount;
        }

        private void Apply(OrderTotalAmountChangedDomainEvent @event)
        {
            TotalAmount = @event.TotalAmount;
        }

        private void Apply(OrderDeletedDomainEvent @event)
        {
            OrderCode = @event.OrderCode;
        }
    }
}
