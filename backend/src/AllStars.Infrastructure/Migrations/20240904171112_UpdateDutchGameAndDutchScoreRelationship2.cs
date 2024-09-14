using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllStars.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDutchGameAndDutchScoreRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DutchScores_Users_DutchGameId",
                table: "DutchScores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "DutchGames",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DutchScores_PlayerId",
                table: "DutchScores",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DutchScores_Users_PlayerId",
                table: "DutchScores",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DutchScores_Users_PlayerId",
                table: "DutchScores");

            migrationBuilder.DropIndex(
                name: "IX_DutchScores_PlayerId",
                table: "DutchScores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "DutchGames",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DutchScores_Users_DutchGameId",
                table: "DutchScores",
                column: "DutchGameId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
