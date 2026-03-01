using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainerHoursApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchAndNotesToTrainerHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TrainerHours",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TrainerHours");
        }
    }
}
