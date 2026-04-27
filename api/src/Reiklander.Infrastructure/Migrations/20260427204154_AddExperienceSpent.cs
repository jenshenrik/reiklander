using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reiklander.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExperienceSpent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperienceSpent",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceTotal",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceSpent",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ExperienceTotal",
                table: "Characters");
        }
    }
}
