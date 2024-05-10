using CrystalSharp.Application;
using CrystalSharpReadModelStoreSqlServerExample.Application.Responses;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Commands
{
    public class CreateCustomerCommand : ICommand<CommandExecutionResult<CustomerResponse>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
