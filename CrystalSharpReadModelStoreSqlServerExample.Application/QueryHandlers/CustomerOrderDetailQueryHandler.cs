using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Application;
using CrystalSharp.Application.Handlers;
using CrystalSharp.Infrastructure.ReadModelStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Queries;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.QueryHandlers
{
    public class CustomerOrderDetailQueryHandler : QueryHandler<CustomerOrderDetailQuery, CustomerOrderReadModel>
    {
        private readonly IReadModelStore<int> _readModelStore;

        public CustomerOrderDetailQueryHandler(IReadModelStore<int> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public override async Task<QueryExecutionResult<CustomerOrderReadModel>> Handle(CustomerOrderDetailQuery request, CancellationToken cancellationToken = default)
        {
            if (request == null) return await Fail("Invalid query.");

            CustomerOrderReadModel readModel = await _readModelStore.Find<CustomerOrderReadModel>(request.OrderGlobaUId, cancellationToken).ConfigureAwait(false);

            if (readModel == null)
            {
                return await Fail("Customer order not found.");
            }

            return await Ok(readModel);
        }
    }
}
