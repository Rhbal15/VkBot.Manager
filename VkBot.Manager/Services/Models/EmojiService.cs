using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VkBot.Manager.Data;

namespace VkBot.Manager.Services.Models
{
    public class EmojiService : IEmojiService
    {
        private readonly ApplicationDbContext _context;

        public EmojiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Emoji> GetAll()
        {
            return _context.Emojis;
        }

        public Emoji Get(int id)
        {
            return _context.Emojis.FirstOrDefault(p => p.Id == id);
        }

        public Emoji GetBySymbol(string symbol)
        {
            var emoji = _context.Emojis.ToList().FirstOrDefault(p => p.Symbol == symbol);

            if (emoji != null)
            {
                return emoji;
            }

            emoji = _context.Add(new Emoji
            {
                Symbol = symbol
            }).Entity;

            _context.SaveChanges();

            return emoji;
        }

        public bool IsExist(string symbol)
        {
            return _context.Emojis.ToList().Any(p => p.Symbol == symbol);
        }

        public IEnumerable<Emoji> GetAvaliableStickerSetEmojis(int stickerSetId)
        {
            return _context.StickerEmojis
                .Include(p => p.Emoji)
                .Include(p => p.Sticker).ThenInclude(p => p.StickerSet)
                .Where(p => p.Sticker.StickerSet.Id == stickerSetId)
                .Select(p => p.Emoji).AsEnumerable()
                .Distinct((first, second) => first.Symbol == second.Symbol);
        }
    }
}