using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false, comment: "The id of the author")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the author"),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Biography of the author"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of birth of the author")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "ID of the genre")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The name of the genre"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "A description of the genre")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false, comment: "The id of the member")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the member"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The email of the member"),
                    MembershipDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the membership started"),
                    ContactInfo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "The contact information of the member")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false, comment: "The id of the book")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The title of the book"),
                    ISBN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false, comment: "The ISBN of the book"),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The publication date of the book"),
                    AuthorId = table.Column<int>(type: "int", nullable: false, comment: "The id of the author"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "The id of the genre"),
                    AvailabilityStatus = table.Column<bool>(type: "bit", nullable: false, comment: "Availability status of the book")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LendingRecords",
                columns: table => new
                {
                    LendingRecordId = table.Column<int>(type: "int", nullable: false, comment: "The id of the lending record")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false, comment: "The id of the borrowed book"),
                    MemberId = table.Column<int>(type: "int", nullable: false, comment: "The id of the member who borrowed the book"),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the book was borrowed"),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "The date when the book was returned")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendingRecords", x => x.LendingRecordId);
                    table.ForeignKey(
                        name: "FK_LendingRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendingRecords_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Biography" },
                    { 2, null, "Horror" },
                    { 3, null, "Crime" },
                    { 4, null, "History" },
                    { 5, null, "Romance" },
                    { 6, null, "Classic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_LendingRecords_BookId",
                table: "LendingRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_LendingRecords_MemberId",
                table: "LendingRecords",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendingRecords");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
