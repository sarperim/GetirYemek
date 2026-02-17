using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}
