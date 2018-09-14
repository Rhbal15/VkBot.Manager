using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VkBot.Manager.Data;

namespace VkBot.Manager.Services.Models
{
    public class KeyboardService : IKeyboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmojiService _emojiService;

        public KeyboardService(ApplicationDbContext context, IEmojiService emojiService)
        {
            _context = context;
            _emojiService = emojiService;
        }

        public void CreateAndActivateKeyboard(string emojiSequence)
        {
            var emojiSymbolds = emojiSequence.Split(" ");

            var emojis = emojiSymbolds.Select(p => _emojiService.GetBySymbol(p));

            DeactivateAllKeyboards();

            var keyboard = new Keyboard
            {
                KeyboardStatus = KeyboardStatus.Active
            };

            keyboard.Buttons = emojis.Select((emoji, i) => new KeyboardButton
            {
                Emoji = emoji,
                Postition = i,
                Keyboard = keyboard
            }).ToList();

            _context.AddRange(keyboard.Buttons);
            _context.AddRange(keyboard);
            _context.SaveChanges();
        }

        public void DeactivateAllKeyboards()
        {
            foreach (var keyboard in _context.Keyboards.Where(p => p.KeyboardStatus == KeyboardStatus.Active))
            {
                keyboard.KeyboardStatus = KeyboardStatus.Inactive;
                _context.Update(keyboard);
            }

            _context.SaveChanges();
        }

        public Keyboard GetActiveKeyboard()
        {
            return _context.Keyboards.Include(p => p.Buttons).ThenInclude(p => p.Emoji)
                .FirstOrDefault(p => p.KeyboardStatus == KeyboardStatus.Active);
        }

        public IEnumerable<Emoji> GetActiveKeyboardEmojis()
        {
            return GetActiveKeyboard()?.Buttons.Select(p => p.Emoji);
        }
    }
}