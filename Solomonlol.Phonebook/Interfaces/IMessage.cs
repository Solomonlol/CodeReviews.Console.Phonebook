using Backend.Models;
using Backend.Models.Dto;

namespace Backend.Interfaces
{
    public interface IMessage
    {
        Task SendMessageAsync(string header, string message, User sender, ContactDto receiver);
    }
}
