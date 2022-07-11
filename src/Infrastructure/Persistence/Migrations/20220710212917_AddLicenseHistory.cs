using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvCodingCase.Infrastructure.Persistence.Migrations
{
    public partial class AddLicenseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false, comment: "Unique identifier of a user."),
                    LatestLicense = table.Column<string>(type: "text", nullable: false, comment: "The latest license info of the user.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.UserId);
                },
                comment: "Contains information of a license.");

            migrationBuilder.CreateIndex(
                name: "IX_License_UserId",
                table: "License",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "License");
        }
    }
}
