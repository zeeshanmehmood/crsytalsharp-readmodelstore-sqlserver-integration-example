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
    public class DeleteOrderCommandHandler : CommandHandler<DeleteOrderCommand, DeleteOrderResponse>
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public DeleteOrderCommandHandler(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public override async Task<CommandExecutionResult<DeleteOrderResponse>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken = default)
        {
            if (request == null) return await Fail("Invalid command.");

            Order order = await _eventStore.Get<Order>(request.GlobalUId, cancellationToken).ConfigureAwait(false);

            if (order == null)
            {
                return await Fail("Order not found.");
            }

            order.Delete();
            await _eventStore.Delete<Order>(order, cancellationToken).ConfigureAwait(false);

            DeleteOrderResponse response = new() { GlobalUId = order.GlobalUId };

            return await Ok(response);
        }
    }
}
