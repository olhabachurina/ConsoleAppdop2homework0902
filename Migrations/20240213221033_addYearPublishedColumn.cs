using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppdop2homework0902.Migrations
{
    /// <inheritdoc />
    public partial class addYearPublishedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearPublished",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearPublished",
                table: "Books");
        }
    }
}
