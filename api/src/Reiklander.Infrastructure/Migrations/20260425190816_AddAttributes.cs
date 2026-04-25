using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reiklander.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Agility_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Agility_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Agility_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Agility_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BallisticSkill_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BallisticSkill_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BallisticSkill_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BallisticSkill_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dexterity_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dexterity_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dexterity_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dexterity_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fellowship_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fellowship_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fellowship_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fellowship_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Initiative_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Initiative_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Initiative_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Initiative_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toughness_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toughness_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toughness_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toughness_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponSkill_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponSkill_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponSkill_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponSkill_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Willpower_Advances",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Willpower_Bonus",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Willpower_CostToAdvance",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Willpower_Value",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agility_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Agility_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Agility_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Agility_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BallisticSkill_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BallisticSkill_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BallisticSkill_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BallisticSkill_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Dexterity_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Dexterity_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Dexterity_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Dexterity_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Fellowship_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Fellowship_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Fellowship_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Fellowship_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Initiative_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Initiative_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Initiative_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Initiative_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Intelligence_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Intelligence_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Intelligence_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Intelligence_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Strength_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Strength_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Strength_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Strength_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Toughness_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Toughness_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Toughness_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Toughness_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "WeaponSkill_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "WeaponSkill_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "WeaponSkill_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "WeaponSkill_Value",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Willpower_Advances",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Willpower_Bonus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Willpower_CostToAdvance",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Willpower_Value",
                table: "Characters");
        }
    }
}
