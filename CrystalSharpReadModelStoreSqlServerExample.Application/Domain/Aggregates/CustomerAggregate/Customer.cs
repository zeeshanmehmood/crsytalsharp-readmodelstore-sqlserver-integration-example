using CrystalSharp.Domain;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.CustomerAggregate.Events;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.CustomerAggregate
{
    public class Customer : AggregateRoot<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }

        private static void ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
            {
                customer.ThrowDomainException("Customer name is required.");
            }

            if (string.IsNullOrEmpty(customer.Address))
            {
                customer.ThrowDomainException("Address is required.");
            }
        }

        public static Customer Create(string name, string address)
        {
            Customer customer = new() { Name = name, Address = address };

            ValidateCustomer(customer);

            customer.Raise(new CustomerCreatedDomainEvent(customer.GlobalUId, customer.Name, customer.Address));

            return customer;
        }

        public override void Delete()
        {
            base.Delete();
        }

        private void Apply(CustomerCreatedDomainEvent @event)
        {
            Name = @event.Name;
            Address = @event.Address;
        }
    }
}
