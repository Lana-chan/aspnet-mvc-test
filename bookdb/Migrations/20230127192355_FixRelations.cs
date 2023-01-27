using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookdb.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId1",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_ApplicationUserId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_ApplicationUserId1",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Book");

            migrationBuilder.CreateTable(
                name: "ApplicationUserBook",
                columns: table => new
                {
                    OwnedBooksId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersOwnedId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserBook", x => new { x.OwnedBooksId, x.UsersOwnedId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook_AspNetUsers_UsersOwnedId",
                        column: x => x.UsersOwnedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook_Book_OwnedBooksId",
                        column: x => x.OwnedBooksId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserBook1",
                columns: table => new
                {
                    UsersWantedId = table.Column<string>(type: "TEXT", nullable: false),
                    WantedBooksId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserBook1", x => new { x.UsersWantedId, x.WantedBooksId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook1_AspNetUsers_UsersWantedId",
                        column: x => x.UsersWantedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook1_Book_WantedBooksId",
                        column: x => x.WantedBooksId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserBook_UsersOwnedId",
                table: "ApplicationUserBook",
                column: "UsersOwnedId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserBook1_WantedBooksId",
                table: "ApplicationUserBook1",
                column: "WantedBooksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserBook");

            migrationBuilder.DropTable(
                name: "ApplicationUserBook1");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Book",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Book",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_ApplicationUserId",
                table: "Book",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ApplicationUserId1",
                table: "Book",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId",
                table: "Book",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_ApplicationUserId1",
                table: "Book",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
