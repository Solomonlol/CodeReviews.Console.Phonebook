using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    internal interface IMessage
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }

        Task SendMessageAsync(string message, string sender, string receiver);
    }
}
