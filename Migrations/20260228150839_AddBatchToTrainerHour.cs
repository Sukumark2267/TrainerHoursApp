using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerHoursApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchToTrainerHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "TrainerHours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batch",
                table: "TrainerHours");
        }
    }
}
