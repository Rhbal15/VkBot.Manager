using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VkBot.Manager.Data;
using VkBot.Manager.ViewModels.BotViewModels;

namespace VkBot.Manager.Services.Models
{
    public class BotUserService : IBotUserService
    {
        private readonly ApplicationDbContext _context;

        public BotUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BotUser Get(long vkUserId)
        {
            var bot = _context.BotUsers.FirstOrDefault(p => p.VkId == vkUserId) ?? Create(new BotUser
            {
                VkId = vkUserId
            });

            return bot;
        }

        public BotUser Create(BotUser user)
        {
            user = _context.Add(user).Entity;

            _context.SaveChanges();

            return user;
        }

        public bool IsExist(long vkUserId)
        {
            return _context.BotUsers.Any(p => p.VkId == vkUserId);
        }

        public void CreateSubscription(long vkUserId, JoinType? joinType)
        {
            if (joinType == null)
            {
                return;
            }

            if (IsAlreadyAdded(vkUserId, joinType))
            {
                return;
            }

            var vkUser = Get(vkUserId);

            var subscription = new Subscription
            {
                BotUser = vkUser,
                JoinDate = DateTime.Now,
                JoinType = joinType
            };

            _context.Add(subscription);

            _context.SaveChanges();
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _context.Subscriptions
                .Include(p => p.BotUser)
                .OrderByDescending(p => p.JoinDate);
        }

        private bool IsAlreadyAdded(long vkUserId, JoinType? joinType)
        {
            var lastSubscription = GetLastSubscription(vkUserId);

            return lastSubscription != null && lastSubscription.JoinType == joinType;
        }

        private Subscription GetLastSubscription(long vkUserId)
        {
            var subscription = _context.Subscriptions
                .Include(p => p.BotUser)
                .Where(p => p.BotUser.VkId == vkUserId)
                .OrderByDescending(p => p.JoinDate)
                .FirstOrDefault();

            return subscription;
        }
    }
}