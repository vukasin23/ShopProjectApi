using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UsecaseLogtableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UseCaseLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    UseCaseId = table.Column<int>(type: "int", nullable: false),
                    UseCaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseCaseLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseCaseLog_UseCases_UseCaseId",
                        column: x => x.UseCaseId,
                        principalTable: "UseCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UseCaseLog_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLog_UseCaseId",
                table: "UseCaseLog",
                column: "UseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLog_userId",
                table: "UseCaseLog",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UseCaseLog");
        }
    }
}
