using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CrystalSharp.Application;
using CrystalSharp.Application.Handlers;
using CrystalSharp.Domain;
using CrystalSharp.Infrastructure;
using CrystalSharp.Infrastructure.Paging;
using CrystalSharp.Infrastructure.ReadModelStoresPersistence;
using CrystalSharpReadModelStoreSqlServerExample.Application.Queries;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.QueryHandlers
{
    public class CustomerOrderListQueryHandler : QueryHandler<CustomerOrderListQuery, PagedResult<CustomerOrderReadModel>>
    {
        private readonly IReadModelStore<int> _readModelStore;

        public CustomerOrderListQueryHandler(IReadModelStore<int> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public override async Task<QueryExecutionResult<PagedResult<CustomerOrderReadModel>>> Handle(CustomerOrderListQuery request, CancellationToken cancellationToken = default)
        {
            if (request == null) return await Fail("Invalid query.");

            Expression<Func<CustomerOrderReadModel, bool>> predicate = x => x.CustomerId == request.CustomerGlobalUId && x.EntityStatus == EntityStatus.Active;
            long totalRows = await _readModelStore.Count(predicate, cancellationToken).ConfigureAwait(false);
            int totalPages = (int)Math.Ceiling((double)totalRows / request.PageSize);

            if (request.CurrentPage < 1)
            {
                request.CurrentPage = 1;
            }

            if (totalPages > 0 && request.CurrentPage > totalPages)
            {
                request.CurrentPage = totalPages;
            }

            if (request.PageSize < 1)
            {
                request.PageSize = 1;
            }

            int skip = (request.CurrentPage - 1) * request.PageSize;
            int take = request.PageSize;
            PagedResult<CustomerOrderReadModel> readModel = await _readModelStore.Get(skip, take, predicate, RecordMode.Active, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (readModel == null || readModel.Data == null)
            {
                return await Fail("No orders were found for this customer.");
            }

            return await Ok(readModel);
        }
    }
}
