using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Manager.Data;

namespace VkBot.Manager.Models
{
    public class ShowedSticker
    {
        public int Id { get; set; }
        public Sticker Sticker { get; set; }
        public Emoji Emoji { get; set; }
        public BotUser BotUser { get; set; }
    }
}
