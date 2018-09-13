using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VkBot.Manager.Data;
using VkBot.Manager.Models;
using VkBot.Manager.Services.Telegram;
using VkBot.Manager.Services.Vk;

namespace VkBot.Manager.Services.Models
{
    public class StickerService : IStickerService
    {
        private readonly ITelegramService _telegramService;
        private readonly IVkGroupPhotoService _vkGroupPhotoService;
        private readonly ApplicationDbContext _context;
        private readonly IEmojiService _emojiService;
        private readonly IAzureImageService _azureImageService;

        public StickerService(
            ITelegramService telegramService,
            IVkGroupPhotoService vkGroupPhotoService,
            ApplicationDbContext context,
            IEmojiService emojiService,
            IAzureImageService azureImageService)
        {
            _telegramService = telegramService;
            _vkGroupPhotoService = vkGroupPhotoService;
            _context = context;
            _emojiService = emojiService;
            _azureImageService = azureImageService;
        }

        public async Task<int> LoadStickerSet(string stickerSetName)
        {
            var telegramStickerSet = await _telegramService.GetStickerSetAsync(stickerSetName);

            var stickerSet = CreateStickerSet(telegramStickerSet.Title, telegramStickerSet.Name);

            foreach (var sticker in telegramStickerSet.Stickers)
            {
                var memoryStream = new MemoryStream();
                await _telegramService.DownloadSticker(sticker.FileId, memoryStream);
                memoryStream.Position = 0;
                var uri = await _azureImageService.UploadImage($"{sticker.FileId}.webp", memoryStream);

                CreateSticker(sticker.Emoji, sticker.FileId, null,
                    stickerSet.Id, azureImageUrl: uri.AbsoluteUri);
            }

            return stickerSet.Id;
        }

        public StickerSet CreateStickerSet(string stickerSetTitle, string stickerSetName, long? albumId = null)
        {
            var stickerSet = new StickerSet
            {
                VkAlbumId = albumId,
                Name = stickerSetName,
                Title = stickerSetTitle,
                CreatedDate = DateTime.Now
            };

            stickerSet = _context.Add(stickerSet).Entity;

            _context.SaveChangesAsync();

            return stickerSet;
        }

        public Sticker CreateSticker(string stickerEmoji,
            string stickerFileId,
            long? photoId,
            int? stickerSetId = null,
            StickerStatus stickerStatus = StickerStatus.Unpublished,
            string azureImageUrl = "")
        {
            var sticker = new Sticker
            {
                VkImageId = photoId,
                TelegramFileId = stickerFileId,
                CreatedDate = DateTime.Now,
                StickerStatus = stickerStatus,
                AzureImageUrl = azureImageUrl
            };

            if (stickerSetId != null)
            {
                sticker.StickerSet = _context.StickerSets.FirstOrDefault(p => p.Id == stickerSetId);
            }

            var emoji = new StickerEmoji
            {
                Emoji = _emojiService.GetBySymbol(stickerEmoji),
                Sticker = sticker
            };

            _context.Add(emoji);

            sticker = _context.Add(sticker).Entity;

            _context.SaveChanges();

            return sticker;
        }

        public IEnumerable<StickerSet> GetAllStickerSets()
        {
            return _context.StickerSets.Include(p => p.Stickers).ThenInclude(p => p.Emoji)
                .OrderByDescending(p => p.CreatedDate);
        }

        public Sticker GetStickerByEmoji(string inputEmoji, long botUserId)
        {

            var emojis = _context.Emojis.Include(p => p.Stickers).ThenInclude(p => p.Sticker).ToList();
             
            Emoji emoji = null;

            foreach (var item in emojis)
            {
                if (item.Symbol == inputEmoji)
                {
                    emoji = item;
                }
            }

            if (emoji == null)
            {
                return null;
            }

            var stickers = emoji.Stickers.Select(p => p.Sticker).ToList();

            if (stickers.Count == 0)
            {
                return null;
            }

            var showedStickers =
                _context.ShowedStickers.Where(p => p.Emoji.Id == emoji.Id && p.BotUser.VkId == botUserId);

            var showedStickersEntity = showedStickers.Select(p => p.Sticker);

            var stickerExcept = stickers.Except(showedStickersEntity).ToList();

            
            if (stickerExcept.Count != 0)
            {
                stickers = stickerExcept;                
            }
            else
            {
                _context.RemoveRange(showedStickers);
            }

            var count = stickers.Count;

            var next = new Random().Next(count);

            var sticker = stickers.ElementAt(next);

            _context.ShowedStickers.Add(new ShowedSticker()
            {
                Sticker = sticker,
                BotUser = _context.BotUsers.FirstOrDefault(p => p.VkId == botUserId),
                Emoji = emoji
            });

            _context.SaveChanges();

            return sticker;
        }

        public StickerSet GetStickerSet(int id)
        {
            return _context.StickerSets.Include(p => p.Stickers).ThenInclude(p => p.Emoji).ThenInclude(p => p.Emoji)
                .FirstOrDefault(p => p.Id == id);
        }

        public void DeleteSticker(int id)
        {
            var sticker = _context.Stickers.Include(p => p.Emoji).FirstOrDefault(p => p.Id == id);

            if (sticker == null)
            {
                return;
            }

            _vkGroupPhotoService.RemovePhoto(sticker.VkImageId);

            _context.RemoveRange(sticker.Emoji);
            _context.Remove(sticker);
            _context.SaveChanges();
        }

        public Sticker GetSticker(int id)
        {
            return _context.Stickers.Include(p => p.Emoji).ThenInclude(p => p.Emoji).Include(p => p.StickerSet)
                .FirstOrDefault(p => p.Id == id);
        }

        public int AddEmoji(int stickerId, int emojiId)
        {
            var sticker = _context.Stickers.FirstOrDefault(p => p.Id == stickerId);
            var emoji = _context.Emojis.FirstOrDefault(p => p.Id == emojiId);

            var stickerEmoji = _context.Add(new StickerEmoji
            {
                Sticker = sticker,
                Emoji = emoji
            }).Entity;

            _context.SaveChanges();

            return stickerEmoji.Id;
        }

        public int RemoveEmoji(int stickerEmojiId)
        {
            var stickerEmoji = _context.StickerEmojis.Include(p => p.Emoji)
                .FirstOrDefault(p => p.Id == stickerEmojiId);

            if (stickerEmoji == null)
            {
                return 0;
            }

            var emojiId = stickerEmoji.Emoji.Id;

            _context.Remove(stickerEmoji);

            _context.SaveChanges();

            return emojiId;
        }

        public int GetStickerCountByEmoji(string symbol)
        {
            return GetStickersByEmoji(symbol).Count();
        }

        public IEnumerable<Sticker> GetStickersByEmoji(string symbol)
        {
            return _context.Emojis.Include(p => p.Stickers).ThenInclude(p => p.Sticker).ToList()
                .FirstOrDefault(p => p.Symbol == symbol)?.Stickers.Select(p => p.Sticker);
        }

        public async Task PublishStickerSet(int stickerSetId)
        {
            var stickerSet = await _context.StickerSets.Include(p => p.Stickers).FirstOrDefaultAsync(p => p.Id == stickerSetId);

            if (stickerSet == null || stickerSet.StickerSetStatus == StickerSetStatus.Published)
            {
                return;
            }

            var album = await _vkGroupPhotoService.CreateAlbum(stickerSet.Title);

            stickerSet.VkAlbumId = album.Id;
            _context.Update(stickerSet);

            foreach (var sticker in stickerSet.Stickers)
            {
                var uploadedPhoto = await _vkGroupPhotoService.UploadImage(album.Id, sticker.AzureImageUrl);
                sticker.VkImageId = uploadedPhoto.Id;
                _context.Update(sticker);
            }

            await _context.SaveChangesAsync();
        }

        public void DeleteStickerSet(int stickerSetId)
        {
            var stickerSet = _context.StickerSets.Include(p => p.Stickers).FirstOrDefault(p => p.Id == stickerSetId);

            if (stickerSet == null)
            {
                return;
            }

            foreach (var sticker in stickerSet.Stickers.ToList())
            {
                DeleteSticker(sticker.Id);
            }

            _context.Remove(stickerSet);

            _context.SaveChanges();
        }
    }
}