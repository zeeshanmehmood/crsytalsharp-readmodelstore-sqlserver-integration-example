using System;
using CrystalSharp.Application;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Queries
{
    public class CustomerOrderDetailQuery : IQuery<QueryExecutionResult<CustomerOrderReadModel>>
    {
        public Guid OrderGlobaUId { get; set; }
    }
}
