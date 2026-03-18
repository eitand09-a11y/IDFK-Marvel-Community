using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDFK.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingsAndPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Movies",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RatingsCount",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserProfilePicture",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RatingsCount",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UserProfilePicture",
                table: "ChatMessages");
        }
    }
}
