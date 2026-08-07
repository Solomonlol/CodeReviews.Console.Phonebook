using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    internal interface IMessageService
    {
        Task SendMessageAsync(string message);
    }
}
