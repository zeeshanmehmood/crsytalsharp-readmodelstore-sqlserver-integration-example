using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Domain.Infrastructure;
using CrystalSharp.Infrastructure.ReadModelStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate.Events;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.EventHandlers
{
    public class OrderTotalAmountChangedDomainEventHandler : ISynchronousDomainEventHandler<OrderTotalAmountChangedDomainEvent>
    {
        private readonly IReadModelStore<int> _readModelStore;

        public OrderTotalAmountChangedDomainEventHandler(IReadModelStore<int> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public async Task Handle(OrderTotalAmountChangedDomainEvent notification, CancellationToken cancellationToken = default)
        {
            CustomerOrderReadModel readModel = await _readModelStore.Find<CustomerOrderReadModel>(notification.StreamId, cancellationToken).ConfigureAwait(false);

            if (readModel != null)
            {
                readModel.TotalAmount = notification.TotalAmount;

                await _readModelStore.Update(readModel, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
