using Contracts.Events;
using MassTransit;
using System;
using System.Threading.Tasks; 

namespace Basket.Consumer
{
    public class UserCreatedConsumer : IConsumer<IUserCreated>
    {
        public Task Consume(ConsumeContext<IUserCreated> context)
        {
            Console.WriteLine($"UserCreatedConsumer received user");

            return Task.CompletedTask;
        }
    }
}
