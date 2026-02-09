using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Events
{
    public interface IUserCreated
    {
        Guid UserId { get; }
    }
}
