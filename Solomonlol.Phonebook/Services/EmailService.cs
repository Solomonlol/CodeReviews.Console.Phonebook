using Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services
{
    internal class EmailService : IMessage
    {
        public string Sender { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Receiver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task SendMessageAsync(string message, string sender, string receiver)
        {
            throw new NotImplementedException();
        }
    }
}
