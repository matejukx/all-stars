using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllStars.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "DutchScores",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "DutchScores");
        }
    }
}
