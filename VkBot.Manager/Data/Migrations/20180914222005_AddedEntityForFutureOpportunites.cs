using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class AddedEntityForFutureOpportunites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmojiGroupId",
                table: "Emojis",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Advices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmojiDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    EmojiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmojiDescriptions_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmojiGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmojiInvolves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvolveDate = table.Column<DateTime>(nullable: false),
                    BotUserId = table.Column<int>(nullable: true),
                    EmojiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiInvolves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmojiInvolves_BotUsers_BotUserId",
                        column: x => x.BotUserId,
                        principalTable: "BotUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmojiInvolves_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Intents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdviceConditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdviceId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    NumberSendedSticker = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviceConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdviceConditions_Advices_AdviceId",
                        column: x => x.AdviceId,
                        principalTable: "Advices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdviceResponses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Priority = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AdviceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviceResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdviceResponses_Advices_AdviceId",
                        column: x => x.AdviceId,
                        principalTable: "Advices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SendedEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BotUserId = table.Column<int>(nullable: true),
                    SendedDate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    AdviceId = table.Column<int>(nullable: true),
                    StickerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendedEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendedEntities_Advices_AdviceId",
                        column: x => x.AdviceId,
                        principalTable: "Advices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendedEntities_BotUsers_BotUserId",
                        column: x => x.BotUserId,
                        principalTable: "BotUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendedEntities_Stickers_StickerId",
                        column: x => x.StickerId,
                        principalTable: "Stickers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntentSentences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IntentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentSentences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntentSentences_Intents_IntentId",
                        column: x => x.IntentId,
                        principalTable: "Intents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emojis_EmojiGroupId",
                table: "Emojis",
                column: "EmojiGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AdviceConditions_AdviceId",
                table: "AdviceConditions",
                column: "AdviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdviceResponses_AdviceId",
                table: "AdviceResponses",
                column: "AdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmojiDescriptions_EmojiId",
                table: "EmojiDescriptions",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_EmojiInvolves_BotUserId",
                table: "EmojiInvolves",
                column: "BotUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmojiInvolves_EmojiId",
                table: "EmojiInvolves",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_IntentSentences_IntentId",
                table: "IntentSentences",
                column: "IntentId");

            migrationBuilder.CreateIndex(
                name: "IX_SendedEntities_AdviceId",
                table: "SendedEntities",
                column: "AdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_SendedEntities_BotUserId",
                table: "SendedEntities",
                column: "BotUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SendedEntities_StickerId",
                table: "SendedEntities",
                column: "StickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emojis_EmojiGroups_EmojiGroupId",
                table: "Emojis",
                column: "EmojiGroupId",
                principalTable: "EmojiGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emojis_EmojiGroups_EmojiGroupId",
                table: "Emojis");

            migrationBuilder.DropTable(
                name: "AdviceConditions");

            migrationBuilder.DropTable(
                name: "AdviceResponses");

            migrationBuilder.DropTable(
                name: "EmojiDescriptions");

            migrationBuilder.DropTable(
                name: "EmojiGroups");

            migrationBuilder.DropTable(
                name: "EmojiInvolves");

            migrationBuilder.DropTable(
                name: "IntentSentences");

            migrationBuilder.DropTable(
                name: "SendedEntities");

            migrationBuilder.DropTable(
                name: "Intents");

            migrationBuilder.DropTable(
                name: "Advices");

            migrationBuilder.DropIndex(
                name: "IX_Emojis_EmojiGroupId",
                table: "Emojis");

            migrationBuilder.DropColumn(
                name: "EmojiGroupId",
                table: "Emojis");
        }
    }
}
