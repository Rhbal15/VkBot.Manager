using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VkBot.Manager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BotUser> BotUsers { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<ReceivedMessage> ReceivedMessages { get; set; }
        public DbSet<Sticker> Stickers { get; set; }

        public DbSet<Emoji> Emojis { get; set; }
        public DbSet<StickerEmoji> StickerEmojis { get; set; }
        public DbSet<EmojiDescription> EmojiDescriptions { get; set; }
        public DbSet<EmojiInvolve> EmojiInvolves { get; set; }
        public DbSet<EmojiGroup> EmojiGroups { get; set; }

        public DbSet<StickerSet> StickerSets { get; set; }
        public DbSet<KeyboardButton> KeyboardButtons { get; set; }
        public DbSet<ShowedSticker> ShowedStickers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Advice> Advices { get; set; }
        public DbSet<AdviceCondition> AdviceConditions { get; set; }
        public DbSet<SendedStickerAdviceCondition> SendedStickerAdviceConditions { get; set; }
        public DbSet<AdviceResponse> AdviceResponses  { get; set; }

        
        public DbSet<Intent> Intents  { get; set; }
        public DbSet<IntentSentence> IntentSentences  { get; set; }
        public DbSet<SendedAdvice> SendedAdvices  { get; set; }
        public DbSet<SendedEntity> SendedEntities  { get; set; }
        public DbSet<SendedSticker> SendedStickers  { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ShowedSticker>()
                .HasOne(p => p.Sticker)
                .WithMany(t => t.ShowedStickers)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}