using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Application;
using CrystalSharp.Application.Handlers;
using CrystalSharp.Infrastructure.EventStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Commands;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.OrderAggregate;
using CrystalSharpReadModelStoreSqlServerExample.Application.Responses;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.CommandHandlers
{
    public class ChangeTotalAmountCommandHandler : CommandHandler<ChangeTotalAmountCommand, OrderResponse>
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public ChangeTotalAmountCommandHandler(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public override async Task<CommandExecutionResult<OrderResponse>> Handle(ChangeTotalAmountCommand request, CancellationToken cancellationToken = default)
        {
            if (request == null) return await Fail("Invalid command.");

            Order order = await _eventStore.Get<Order>(request.GlobalUId, cancellationToken).ConfigureAwait(false);

            if (order == null)
            {
                return await Fail("Order not found.");
            }

            order.ChangeTotalAmount(request.TotalAmount);
            await _eventStore.Store(order, cancellationToken).ConfigureAwait(false);

            OrderResponse response = new() { GlobalUId = order.GlobalUId, OrderCode = order.OrderCode };

            return await Ok(response);
        }
    }
}
