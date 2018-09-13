using System;
using VkBot.Manager.Models;
using VkBot.Manager.ViewModels.BotViewModels;

namespace VkBot.Manager.Data
{
    public class Subscription
    {
        public int Id { get; set; }

        public BotUser BotUser { get; set; }
        public JoinType? JoinType { get; set; }
        public DateTime JoinDate { get; set; }
    }
}