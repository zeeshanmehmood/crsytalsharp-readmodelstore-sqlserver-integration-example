using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Domain.Infrastructure;
using CrystalSharp.Infrastructure.EventStoresPersistence;
using CrystalSharp.Infrastructure.ReadModelStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.CustomerAggregate;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.EventHandlers
{
    public class OrderPlacedDomainEventHandler : ISynchronousDomainEventHandler<OrderPlacedDomainEvent>
    {
        private readonly IAggregateEventStore<int> _eventStore;
        private readonly IReadModelStore<int> _readModelStore;

        public OrderPlacedDomainEventHandler(IAggregateEventStore<int> eventStore, IReadModelStore<int> readModelStore)
        {
            _eventStore = eventStore;
            _readModelStore = readModelStore;
        }

        public async Task Handle(OrderPlacedDomainEvent notification, CancellationToken cancellationToken = default)
        {
            Customer customer = await _eventStore.Get<Customer>(notification.CustomerId, cancellationToken).ConfigureAwait(false);

            if (customer != null)
            {
                CustomerOrderReadModel readModel = new()
                {
                    GlobalUId = notification.StreamId,
                    OrderCode = notification.OrderCode,
                    CustomerId = notification.CustomerId,
                    CustomerName = customer.Name,
                    CustomerAddress = customer.Address,
                    Item = notification.Item,
                    TotalAmount = notification.TotalAmount
                };

                await _readModelStore.Store(readModel, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
