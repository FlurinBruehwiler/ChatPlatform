using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatPlatformBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Messages",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Messages");
        }
    }
}
