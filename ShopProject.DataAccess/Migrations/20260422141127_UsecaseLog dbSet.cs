using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UsecaseLogdbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UseCaseLog_UseCases_UseCaseId",
                table: "UseCaseLog");

            migrationBuilder.DropForeignKey(
                name: "FK_UseCaseLog_Users_userId",
                table: "UseCaseLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UseCaseLog",
                table: "UseCaseLog");

            migrationBuilder.RenameTable(
                name: "UseCaseLog",
                newName: "UseCaseLogs");

            migrationBuilder.RenameIndex(
                name: "IX_UseCaseLog_userId",
                table: "UseCaseLogs",
                newName: "IX_UseCaseLogs_userId");

            migrationBuilder.RenameIndex(
                name: "IX_UseCaseLog_UseCaseId",
                table: "UseCaseLogs",
                newName: "IX_UseCaseLogs_UseCaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UseCaseLogs",
                table: "UseCaseLogs",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UseCaseLogs_UseCases_UseCaseId",
                table: "UseCaseLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UseCaseLogs_Users_userId",
                table: "UseCaseLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UseCaseLogs",
                table: "UseCaseLogs");

            migrationBuilder.RenameTable(
                name: "UseCaseLogs",
                newName: "UseCaseLog");

            migrationBuilder.RenameIndex(
                name: "IX_UseCaseLogs_userId",
                table: "UseCaseLog",
                newName: "IX_UseCaseLog_userId");

            migrationBuilder.RenameIndex(
                name: "IX_UseCaseLogs_UseCaseId",
                table: "UseCaseLog",
                newName: "IX_UseCaseLog_UseCaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UseCaseLog",
                table: "UseCaseLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UseCaseLog_UseCases_UseCaseId",
                table: "UseCaseLog",
                column: "UseCaseId",
                principalTable: "UseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseCaseLog_Users_userId",
                table: "UseCaseLog",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
