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
    public class PlaceOrderCommandHandler : CommandHandler<PlaceOrderCommand, OrderResponse>
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public PlaceOrderCommandHandler(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public override async Task<CommandExecutionResult<OrderResponse>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken = default)
        {
            if (request == null) return await Fail("Invalid command.");

            Order order = Order.PlaceOrder(request.CustomerId, request.Item, request.TotalAmount);

            await _eventStore.Store(order, cancellationToken).ConfigureAwait(false);

            OrderResponse response = new() { GlobalUId = order.GlobalUId, OrderCode = order.OrderCode };

            return await Ok(response);
        }
    }
}
