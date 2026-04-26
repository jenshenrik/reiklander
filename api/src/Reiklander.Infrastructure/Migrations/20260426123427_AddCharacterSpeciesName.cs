using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reiklander.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterSpeciesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Characters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Species",
                table: "Characters");
        }
    }
}
