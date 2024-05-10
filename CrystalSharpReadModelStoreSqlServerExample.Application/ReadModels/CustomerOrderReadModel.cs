using System;
using CrystalSharp.Infrastructure.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels
{
    public class CustomerOrderReadModel : ReadModel<int>
    {
        public string OrderCode { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Item { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
