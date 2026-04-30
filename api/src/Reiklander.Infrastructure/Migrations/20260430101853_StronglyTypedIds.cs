using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reiklander.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StronglyTypedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AggregateId",
                table: "Events",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AggregateId",
                table: "Events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
