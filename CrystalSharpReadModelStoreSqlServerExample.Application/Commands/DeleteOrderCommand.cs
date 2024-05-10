using System;
using CrystalSharp.Application;
using CrystalSharpReadModelStoreSqlServerExample.Application.Responses;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Commands
{
    public class DeleteOrderCommand : ICommand<CommandExecutionResult<DeleteOrderResponse>>
    {
        public Guid GlobalUId { get; set; }
    }
}
