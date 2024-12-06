using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuthorsAndGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Biography", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 1, "Famous author of the Harry Potter series.", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K. Rowling" },
                    { 2, "Author of A Song of Ice and Fire.", new DateTime(1948, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "George R.R. Martin" },
                    { 3, "Widely known author for horror novels.", new DateTime(1955, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stephen King" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);
        }
    }
}
