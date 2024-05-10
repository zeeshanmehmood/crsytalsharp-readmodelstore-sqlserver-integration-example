using System;

namespace CrystalSharpReadModelStoreSqlServerExample.Api.Dto
{
    public class PlaceOrderRequest
    {
        public Guid CustomerGlobalUId { get; set; }
        public string Item { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
