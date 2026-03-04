using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerHoursApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingFields2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batch",
                table: "TrainerHours");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "TrainerHours",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TrainingTitle",
                table: "TrainerHours",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "TrainerHours",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "TrainerHours",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "TrainerHours",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TrainerHours",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PlannedHours",
                table: "TrainerHours",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "TrainerHours",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "TrainerHours",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TrainerHours");

            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "TrainerHours");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "TrainerHours");

            migrationBuilder.DropColumn(
                name: "PlannedHours",
                table: "TrainerHours");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TrainerHours");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "TrainerHours");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "TrainerHours",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrainingTitle",
                table: "TrainerHours",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "TrainerHours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "TrainerHours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
