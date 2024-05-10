using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Domain.Infrastructure;
using CrystalSharp.Infrastructure.ReadModelStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.EventHandlers
{
    public class OrderDeletedDomainEventHandler : ISynchronousDomainEventHandler<OrderDeletedDomainEvent>
    {
        private readonly IReadModelStore<int> _readModelStore;

        public OrderDeletedDomainEventHandler(IReadModelStore<int> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public async Task Handle(OrderDeletedDomainEvent notification, CancellationToken cancellationToken = default)
        {
            CustomerOrderReadModel readModel = await _readModelStore.Find<CustomerOrderReadModel>(notification.StreamId, cancellationToken).ConfigureAwait(false);

            if (readModel != null)
            {
                await _readModelStore.SoftDelete<CustomerOrderReadModel>(readModel.GlobalUId, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
