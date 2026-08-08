using Backend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    internal interface IMessage
    {
        Task SendMessageAsync(string header, string message, string password, UserDto sender, ContactDto receiver);
    }
}
