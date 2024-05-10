using System;
using CrystalSharp.Application;
using CrystalSharpReadModelStoreSqlServerExample.Application.Responses;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Commands
{
    public class ChangeTotalAmountCommand : ICommand<CommandExecutionResult<OrderResponse>>
    {
        public Guid GlobalUId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
