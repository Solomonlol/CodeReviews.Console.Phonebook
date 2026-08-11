using Backend.Models;
using Backend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    public interface IMessage
    {
        Task SendMessageAsync(string header, string message, User sender, ContactDto receiver);
    }
}
