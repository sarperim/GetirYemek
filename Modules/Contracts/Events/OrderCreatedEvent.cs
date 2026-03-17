using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid UserId, decimal TotalAmount);
}
