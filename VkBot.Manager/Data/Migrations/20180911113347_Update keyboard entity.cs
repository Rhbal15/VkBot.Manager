using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class Updatekeyboardentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmileSequence",
                table: "Keyboards");

            migrationBuilder.AddColumn<int>(
                name: "KeyboardStatus",
                table: "Keyboards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KeyboardButtons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Postition = table.Column<int>(nullable: false),
                    KeyboardId = table.Column<int>(nullable: true),
                    EmojiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardButtons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyboardButtons_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyboardButtons_Keyboards_KeyboardId",
                        column: x => x.KeyboardId,
                        principalTable: "Keyboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyboardButtons_EmojiId",
                table: "KeyboardButtons",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyboardButtons_KeyboardId",
                table: "KeyboardButtons",
                column: "KeyboardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyboardButtons");

            migrationBuilder.DropColumn(
                name: "KeyboardStatus",
                table: "Keyboards");

            migrationBuilder.AddColumn<string>(
                name: "SmileSequence",
                table: "Keyboards",
                nullable: true);
        }
    }
}
