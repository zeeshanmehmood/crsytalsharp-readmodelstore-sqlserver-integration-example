using System;

namespace CrystalSharpReadModelStoreSqlServerExample.Api.Dto
{
    public class ChangeTotalAmountRequest
    {
        public Guid OrderGlobalUId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
