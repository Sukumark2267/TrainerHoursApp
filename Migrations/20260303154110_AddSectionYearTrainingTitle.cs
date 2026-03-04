using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerHoursApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionYearTrainingTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainingTitle",
                table: "TrainerHours",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "TrainerHours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingTitle",
                table: "TrainerHours");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "TrainerHours");
        }
    }
}
