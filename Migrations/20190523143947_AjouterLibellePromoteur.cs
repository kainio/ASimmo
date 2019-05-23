using Microsoft.EntityFrameworkCore.Migrations;

namespace ASimmo.Migrations
{
    public partial class AjouterLibellePromoteur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Libelle",
                table: "Promoteurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Libelle",
                table: "Promoteurs");
        }
    }
}
