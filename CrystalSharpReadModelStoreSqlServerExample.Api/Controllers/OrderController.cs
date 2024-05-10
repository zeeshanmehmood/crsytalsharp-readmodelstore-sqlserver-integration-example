using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrystalSharp.Application;
using CrystalSharp.Application.Execution;
using CrystalSharpReadModelStoreSqlServerExample.Api.Dto;
using CrystalSharpReadModelStoreSqlServerExample.Application.Commands;
using CrystalSharpReadModelStoreSqlServerExample.Application.Responses;

namespace CrystalSharpReadModelStoreSqlServerExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;

        public OrderController(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        [HttpPost]
        [Route("place-order")]
        public async Task<ActionResult<CommandExecutionResult<OrderResponse>>> PostPlaceOrder([FromBody] PlaceOrderRequest request)
        {
            PlaceOrderCommand command = new() { CustomerId = request.CustomerGlobalUId, Item = request.Item, TotalAmount = request.TotalAmount };

            return await _commandExecutor.Execute(command, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("change-totalamount")]
        public async Task<ActionResult<CommandExecutionResult<OrderResponse>>> PutChangeTotal([FromBody] ChangeTotalAmountRequest request)
        {
            ChangeTotalAmountCommand command = new() { GlobalUId = request.OrderGlobalUId, TotalAmount = request.TotalAmount };

            return await _commandExecutor.Execute(command, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpDelete]
        [Route("{orderGlobalUId}")]
        public async Task<ActionResult<CommandExecutionResult<DeleteOrderResponse>>> DeleteChangeTotal(Guid orderGlobalUId)
        {
            DeleteOrderCommand command = new() { GlobalUId = orderGlobalUId };

            return await _commandExecutor.Execute(command, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
