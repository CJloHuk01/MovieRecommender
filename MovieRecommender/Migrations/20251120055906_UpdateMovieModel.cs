using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRecommender.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 2000);

            migrationBuilder.Sql(@"
        UPDATE ""Movies"" 
        SET ""ReleaseYear"" = EXTRACT(YEAR FROM ""ReleaseDate"")
    ");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"
        UPDATE ""Movies"" 
        SET ""ReleaseDate"" = MAKE_DATE(""ReleaseYear"", 1, 1)
    ");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Movies");
        }
    }
}
