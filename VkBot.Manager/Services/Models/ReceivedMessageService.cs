using System.Linq;
using Microsoft.EntityFrameworkCore;
using VkBot.Manager.Data;
using VkBot.Manager.Models;

namespace VkBot.Manager.Services.Models
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly ApplicationDbContext _context;

        public ReceivedMessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ReceivedMessage Create(ReceivedMessage message)
        {
            message = _context.Add(message).Entity;

            _context.SaveChanges();

            return message;
        }

        public bool IsReceived(long vkUserId, long vkMessageId)
        {
            return _context.ReceivedMessages.Include(p => p.BotUser)
                .Any(p => p.MessageId == vkMessageId && p.BotUser.VkId == vkUserId);
        }

        public bool IsFirstMessage(long vkUserId, long vkMessageId)
        {
            var messageCount = _context.ReceivedMessages.Include(p => p.BotUser).Count(p => p.BotUser.VkId == vkUserId);

            if (messageCount > 1)
            {
                return false;
            }

            return _context.ReceivedMessages.Include(p => p.BotUser)
                .Any(p => p.BotUser.VkId == vkUserId && p.MessageId == vkMessageId);
        }
    }
}