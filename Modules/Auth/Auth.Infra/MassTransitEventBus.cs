using Auth.Application.Interfaces;
using Auth.Application.Interfaces.Repository;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infra
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _uow;

        public MassTransitEventBus(IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
        {
            _publishEndpoint = publishEndpoint;
            _uow = unitOfWork;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class
        {
            return _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
