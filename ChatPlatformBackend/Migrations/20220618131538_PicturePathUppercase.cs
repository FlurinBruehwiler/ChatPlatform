using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatPlatformBackend.Migrations
{
    public partial class PicturePathUppercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "picturePath",
                table: "Users",
                newName: "PicturePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PicturePath",
                table: "Users",
                newName: "picturePath");
        }
    }
}
