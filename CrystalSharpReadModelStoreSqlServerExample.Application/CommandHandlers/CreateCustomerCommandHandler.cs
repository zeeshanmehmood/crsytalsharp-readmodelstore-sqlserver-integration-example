using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Application;
using CrystalSharp.Application.Handlers;
using CrystalSharp.Infrastructure.EventStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Commands;
using CrystalSharpReadModelStoreSqlServerExample.Application.Domain.Aggregates.CustomerAggregate;
using CrystalSharpReadModelStoreSqlServerExample.Application.Responses;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly IAggregateEventStore<int> _eventStore;

        public CreateCustomerCommandHandler(IAggregateEventStore<int> eventStore)
        {
            _eventStore = eventStore;
        }

        public override async Task<CommandExecutionResult<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            if (request == null) return await Fail("Invalid command.");

            Customer customer = Customer.Create(request.Name, request.Address);

            await _eventStore.Store(customer, cancellationToken).ConfigureAwait(false);

            CustomerResponse response = new() { GlobalUId = customer.GlobalUId, Name = customer.Name };

            return await Ok(response);
        }
    }
}
