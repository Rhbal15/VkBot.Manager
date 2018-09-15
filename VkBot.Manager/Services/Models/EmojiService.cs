using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VkBot.Manager.Data;
using VkBot.Manager.Exceptions;
using VkBot.Manager.ViewModels.EmojisViewModels;
using VkNet.Utils;

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

            return emoji ?? CreateEmoji(symbol);
        }

        private Emoji CreateEmoji(string symbol)
        {
            var emoji = _context.Add(new Emoji
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

        public IEnumerable<EmojiGroup> GetGroups()
        {
            var emojiGroups = _context.EmojiGroups.OrderBy(p => p.Priority);

            return emojiGroups;
        }

        public void CreateGroup(CreateGroupEmojiInputModel inputModel)
        {
            CheckName(inputModel);
            CheckPriority(inputModel);

            var emojis = GetEmojisByEmojiSequence(inputModel.EmojiSequence);

            var group = new EmojiGroup
            {
                Name = inputModel.Name,
                Priority = inputModel.Priority,
                CreateDate = DateTime.UtcNow,
                Emojis = emojis.ToCollection()
            };

            _context.Add(group);

            _context.SaveChanges();
        }

        private void CheckName(CreateGroupEmojiInputModel inputModel)
        {
            var isSuchNameAlreadyExist = _context.EmojiGroups.Any(p => p.Name == inputModel.Name);

            if (isSuchNameAlreadyExist)
            {
                throw new SuchGroupNameAlreadyExists();
            }
        }

        private void CheckPriority(CreateGroupEmojiInputModel inputModel)
        {
            var isSuchPrioritAlreadyExist = _context.EmojiGroups.Any(p => p.Priority == inputModel.Priority);

            if (isSuchPrioritAlreadyExist)
            {
                throw new SuchGroupPriorityAlreadyExists();
            }
        }

        public IEnumerable<Emoji> GetEmojisByEmojiSequence(string emojiSequence)
        {
            var emojiSymbols = SplitEmojiSequence(emojiSequence);

            return GetEmojisBySymbols(emojiSymbols);
        }

        public IEnumerable<string> SplitEmojiSequence(string emojiSequence)
        {
            return emojiSequence.Split(" ").Where(p => !string.IsNullOrWhiteSpace(p));
        }

        public IEnumerable<Emoji> GetEmojisBySymbols(IEnumerable<string> symbols)
        {
            var addedEmojis = _context.Emojis.ToList().Where(p => symbols.Contains(p.Symbol));

            var notAddedEmoji = symbols.Where(p => addedEmojis.All(c => c.Symbol != p));

            return addedEmojis.Concat(
                CreateEmojis(notAddedEmoji)
            );
        }

        public IEnumerable<Emoji> CreateEmojis(IEnumerable<string> symbols)
        {
            var emojis = symbols.Select(p => new Emoji
            {
                Symbol = p
            }).ToList();

            _context.AddRange(emojis);

            _context.SaveChanges();

            return emojis;
        }
    }
}