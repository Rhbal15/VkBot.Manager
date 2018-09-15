using System.Linq;
using Microsoft.EntityFrameworkCore;
using VkBot.Manager.Data;
using VkBot.Manager.Exceptions;
using VkBot.Manager.Services.Models;
using VkBot.Manager.ViewModels.EmojisViewModels;
using Xunit;

namespace VkBot.Manger.Tests.Services.Models
{
    public class EmojiServiceTests
    {
        [Fact]
        public void CreateEmojis_SendTwoSymbols_DatabaseContainsEntitiesAndReturnItemsContainsIds()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CreateEmojis_SendTwoSymbols_DatabaseContainsEntitiesAndReturnItemsContainsIds")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var emojiService = new EmojiService(context);

                var emojiSymbols = new[] {"😐", "😌"};

                var emojis = emojiService.CreateEmojis(emojiSymbols);

                Assert.True(emojis.All(p => p.Id != 0));
                Assert.Equal(2, context.Emojis.Count());
            }
        }

        [Fact]
        public void
            GetEmojisByEmojiSequence_SendThreeSymbolsAndDatabaseContainsOne_DatabaseContainsThreeEntitiesAndReturnThreeItems()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    "GetEmojisByEmojiSequence_SendThreeSymbolsAndDatabaseContainsOne_DatabaseContainsThreeEntitiesAndReturnThreeItems")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(new Emoji
                {
                    Symbol = "🤗"
                });

                context.SaveChanges();

                var emojiService = new EmojiService(context);

                const string emojiSequence = "😐 😌 🤗";

                var emojis = emojiService.GetEmojisByEmojiSequence(emojiSequence);

                Assert.Equal(3, emojis.Count());
                Assert.Equal(3, context.Emojis.Count());
            }
        }

        [Fact]
        public void
            GetEmojisBySymbols_SendThreeSymbolsAndDatabaseContainsOne_DatabaseContainsThreeEntitiesAndReturnThreeItems()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    "GetEmojisBySymbols_SendThreeSymbolsAndDatabaseContainsOne_DatabaseContainsThreeEntitiesAndReturnThreeItems")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(new Emoji
                {
                    Symbol = "🤗"
                });

                context.SaveChanges();

                var emojiService = new EmojiService(context);

                var emojiSymbols = new[] {"😐", "😌", "🤗"};

                var emojis = emojiService.GetEmojisBySymbols(emojiSymbols);

                Assert.Equal(3, emojis.Count());
                Assert.Equal(3, context.Emojis.Count());
            }
        }

        [Fact]
        public void
            CreateGroup_GroupWithSuchNameAlreadyExists_ThrowExceptionThatSuchGroupNameExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    "CreateGroup_GroupWithSuchNameAlreadyExists_ThrowExceptionThatSuchGroupNameExists")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(new EmojiGroup
                {
                    Name = "Emotion"
                });

                context.SaveChanges();

                var emojiService = new EmojiService(context);


                Assert.Throws<SuchGroupNameAlreadyExists>(() =>
                {
                    emojiService.CreateGroup(new CreateGroupEmojiInputModel
                    {
                        Name = "Emotion"
                    });
                });
            }
        }

        [Fact]
        public void
            CreateGroup_GroupWithSuchPriorityAlreadyExists_ThrowExceptionThatSuchGroupNameExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    "CreateGroup_GroupWithSuchPriorityAlreadyExists_ThrowExceptionThatSuchGroupNameExists")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(new EmojiGroup
                {
                    Priority = 1
                });

                context.SaveChanges();

                var emojiService = new EmojiService(context);

                Assert.Throws<SuchGroupPriorityAlreadyExists>(() =>
                {
                    emojiService.CreateGroup(new CreateGroupEmojiInputModel
                    {
                        Name = "1",
                        Priority = 1
                    });
                });
            }
        }

        [Fact]
        public void
            CreateGroup_OneNormalGroup_DatabaseContainsGroup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    "CreateGroup_OneNormalGroup_DatabaseContainsGroup")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var emojiService = new EmojiService(context);


                emojiService.CreateGroup(new CreateGroupEmojiInputModel
                {
                    Name = "1",
                    Priority = 1,
                    EmojiSequence = "😐 😌 🤗"
                });

                Assert.Equal(1, context.EmojiGroups.Count());

                var emojiGroup = context.EmojiGroups.Include(p => p.Emojis).FirstOrDefault();

                Assert.NotNull(emojiGroup);
                Assert.Equal("1", emojiGroup.Name);
                Assert.Equal(1, emojiGroup.Priority);
                Assert.Equal(3, emojiGroup.Emojis.Count());
            }
        }
    }
}