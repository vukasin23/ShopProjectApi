using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedUseCaseLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UseCaseLogs_UseCases_UseCaseId",
                table: "UseCaseLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UseCaseLogs_Users_userId",
                table: "UseCaseLogs");

            migrationBuilder.DropIndex(
                name: "IX_UseCaseLogs_UseCaseId",
                table: "UseCaseLogs");

            migrationBuilder.DropIndex(
                name: "IX_UseCaseLogs_userId",
                table: "UseCaseLogs");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "UseCaseLogs");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UseCaseLogs");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "UseCaseLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "UseCaseLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UseCaseLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "UseCaseLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_UseCaseId",
                table: "UseCaseLogs",
                column: "UseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_userId",
                table: "UseCaseLogs",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UseCaseLogs_UseCases_UseCaseId",
                table: "UseCaseLogs",
                column: "UseCaseId",
                principalTable: "UseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseCaseLogs_Users_userId",
                table: "UseCaseLogs",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
