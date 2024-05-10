using System;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Responses
{
    public class OrderResponse
    {
        public Guid GlobalUId { get; set; }
        public string OrderCode { get; set; }
    }
}
