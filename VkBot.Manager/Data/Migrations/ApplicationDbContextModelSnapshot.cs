﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VkBot.Manager.Data;

namespace VkBot.Manager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Advice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Advices");
                });

            modelBuilder.Entity("VkBot.Manager.Data.AdviceCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdviceId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AdviceId")
                        .IsUnique();

                    b.ToTable("AdviceConditions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AdviceCondition");
                });

            modelBuilder.Entity("VkBot.Manager.Data.AdviceResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdviceId");

                    b.Property<int>("Priority");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("AdviceId");

                    b.ToTable("AdviceResponses");
                });

            modelBuilder.Entity("VkBot.Manager.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VkBot.Manager.Data.BotUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<long>("VkId");

                    b.HasKey("Id");

                    b.ToTable("BotUsers");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Emoji", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmojiGroupId");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.HasIndex("EmojiGroupId");

                    b.ToTable("Emojis");
                });

            modelBuilder.Entity("VkBot.Manager.Data.EmojiDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmojiId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("EmojiId");

                    b.ToTable("EmojiDescriptions");
                });

            modelBuilder.Entity("VkBot.Manager.Data.EmojiGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Name");

                    b.Property<int>("Priority");

                    b.HasKey("Id");

                    b.ToTable("EmojiGroups");
                });

            modelBuilder.Entity("VkBot.Manager.Data.EmojiInvolve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BotUserId");

                    b.Property<int?>("EmojiId");

                    b.Property<DateTime>("InvolveDate");

                    b.HasKey("Id");

                    b.HasIndex("BotUserId");

                    b.HasIndex("EmojiId");

                    b.ToTable("EmojiInvolves");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Intent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Intents");
                });

            modelBuilder.Entity("VkBot.Manager.Data.IntentSentence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int?>("IntentId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("IntentId");

                    b.ToTable("IntentSentences");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Keyboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KeyboardStatus");

                    b.HasKey("Id");

                    b.ToTable("Keyboards");
                });

            modelBuilder.Entity("VkBot.Manager.Data.KeyboardButton", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmojiId");

                    b.Property<int?>("KeyboardId");

                    b.Property<int>("Postition");

                    b.HasKey("Id");

                    b.HasIndex("EmojiId");

                    b.HasIndex("KeyboardId");

                    b.ToTable("KeyboardButtons");
                });

            modelBuilder.Entity("VkBot.Manager.Data.ReceivedMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BotUserId");

                    b.Property<long>("MessageId");

                    b.HasKey("Id");

                    b.HasIndex("BotUserId");

                    b.ToTable("ReceivedMessages");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BotUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("SendedDate");

                    b.HasKey("Id");

                    b.HasIndex("BotUserId");

                    b.ToTable("SendedEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SendedEntity");
                });

            modelBuilder.Entity("VkBot.Manager.Data.ShowedSticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BotUserId");

                    b.Property<int?>("EmojiId");

                    b.Property<int?>("StickerId");

                    b.HasKey("Id");

                    b.HasIndex("BotUserId");

                    b.HasIndex("EmojiId");

                    b.HasIndex("StickerId");

                    b.ToTable("ShowedStickers");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Sticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AzureImageUrl");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("StickerSetId");

                    b.Property<int>("StickerStatus");

                    b.Property<string>("TelegramFileId");

                    b.Property<long?>("VkImageId");

                    b.HasKey("Id");

                    b.HasIndex("StickerSetId");

                    b.ToTable("Stickers");
                });

            modelBuilder.Entity("VkBot.Manager.Data.StickerEmoji", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmojiId");

                    b.Property<int?>("StickerId");

                    b.HasKey("Id");

                    b.HasIndex("EmojiId");

                    b.HasIndex("StickerId");

                    b.ToTable("StickerEmojis");
                });

            modelBuilder.Entity("VkBot.Manager.Data.StickerSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.Property<int>("StickerSetStatus");

                    b.Property<string>("Title");

                    b.Property<long?>("VkAlbumId");

                    b.HasKey("Id");

                    b.ToTable("StickerSets");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BotUserId");

                    b.Property<DateTime>("JoinDate");

                    b.Property<int?>("JoinType");

                    b.HasKey("Id");

                    b.HasIndex("BotUserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedStickerAdviceCondition", b =>
                {
                    b.HasBaseType("VkBot.Manager.Data.AdviceCondition");

                    b.Property<int>("NumberSendedSticker");

                    b.ToTable("SendedStickerAdviceCondition");

                    b.HasDiscriminator().HasValue("SendedStickerAdviceCondition");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedAdvice", b =>
                {
                    b.HasBaseType("VkBot.Manager.Data.SendedEntity");

                    b.Property<int?>("AdviceId");

                    b.HasIndex("AdviceId");

                    b.ToTable("SendedAdvice");

                    b.HasDiscriminator().HasValue("SendedAdvice");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedSticker", b =>
                {
                    b.HasBaseType("VkBot.Manager.Data.SendedEntity");

                    b.Property<int?>("StickerId");

                    b.HasIndex("StickerId");

                    b.ToTable("SendedSticker");

                    b.HasDiscriminator().HasValue("SendedSticker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VkBot.Manager.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VkBot.Manager.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VkBot.Manager.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VkBot.Manager.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VkBot.Manager.Data.AdviceCondition", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Advice", "Advice")
                        .WithOne("Condition")
                        .HasForeignKey("VkBot.Manager.Data.AdviceCondition", "AdviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VkBot.Manager.Data.AdviceResponse", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Advice", "Advice")
                        .WithMany("Responses")
                        .HasForeignKey("AdviceId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Emoji", b =>
                {
                    b.HasOne("VkBot.Manager.Data.EmojiGroup", "EmojiGroup")
                        .WithMany("Emojis")
                        .HasForeignKey("EmojiGroupId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.EmojiDescription", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Emoji")
                        .WithMany("EmojiDescriptions")
                        .HasForeignKey("EmojiId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.EmojiInvolve", b =>
                {
                    b.HasOne("VkBot.Manager.Data.BotUser", "BotUser")
                        .WithMany("EmojiInvolves")
                        .HasForeignKey("BotUserId");

                    b.HasOne("VkBot.Manager.Data.Emoji", "Emoji")
                        .WithMany("EmojiInvolves")
                        .HasForeignKey("EmojiId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.IntentSentence", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Intent", "Intent")
                        .WithMany("IntentSentences")
                        .HasForeignKey("IntentId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.KeyboardButton", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Emoji", "Emoji")
                        .WithMany("KeyboardButtons")
                        .HasForeignKey("EmojiId");

                    b.HasOne("VkBot.Manager.Data.Keyboard", "Keyboard")
                        .WithMany("Buttons")
                        .HasForeignKey("KeyboardId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.ReceivedMessage", b =>
                {
                    b.HasOne("VkBot.Manager.Data.BotUser", "BotUser")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("BotUserId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedEntity", b =>
                {
                    b.HasOne("VkBot.Manager.Data.BotUser", "BotUser")
                        .WithMany()
                        .HasForeignKey("BotUserId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.ShowedSticker", b =>
                {
                    b.HasOne("VkBot.Manager.Data.BotUser", "BotUser")
                        .WithMany()
                        .HasForeignKey("BotUserId");

                    b.HasOne("VkBot.Manager.Data.Emoji", "Emoji")
                        .WithMany()
                        .HasForeignKey("EmojiId");

                    b.HasOne("VkBot.Manager.Data.Sticker", "Sticker")
                        .WithMany("ShowedStickers")
                        .HasForeignKey("StickerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VkBot.Manager.Data.Sticker", b =>
                {
                    b.HasOne("VkBot.Manager.Data.StickerSet", "StickerSet")
                        .WithMany("Stickers")
                        .HasForeignKey("StickerSetId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.StickerEmoji", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Emoji", "Emoji")
                        .WithMany("Stickers")
                        .HasForeignKey("EmojiId");

                    b.HasOne("VkBot.Manager.Data.Sticker", "Sticker")
                        .WithMany("Emoji")
                        .HasForeignKey("StickerId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.Subscription", b =>
                {
                    b.HasOne("VkBot.Manager.Data.BotUser", "BotUser")
                        .WithMany("Subscriptions")
                        .HasForeignKey("BotUserId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedAdvice", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Advice", "Advice")
                        .WithMany()
                        .HasForeignKey("AdviceId");
                });

            modelBuilder.Entity("VkBot.Manager.Data.SendedSticker", b =>
                {
                    b.HasOne("VkBot.Manager.Data.Sticker", "Sticker")
                        .WithMany()
                        .HasForeignKey("StickerId");
                });
#pragma warning restore 612, 618
        }
    }
}
