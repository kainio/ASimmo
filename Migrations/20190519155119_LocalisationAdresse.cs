using Microsoft.EntityFrameworkCore.Migrations;

namespace ASimmo.Migrations
{
    public partial class LocalisationAdresse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Lat",
                table: "Adresses",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Lon",
                table: "Adresses",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 1, "Maison" });

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 2, "Appartement" });

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 3, "Terrain" });

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 4, "Studio" });

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 5, "Villa" });

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 6, "Immeuble" });

            migrationBuilder.InsertData(
                table: "TypesBiensImmos",
                columns: new[] { "TypeBienImmoId", "Libelle" },
                values: new object[] { 7, "Résidence" });

            migrationBuilder.InsertData(
                table: "TypesClassifications",
                columns: new[] { "TypeClassificationId", "Libelle" },
                values: new object[] { 1, "Projet" });

            migrationBuilder.InsertData(
                table: "TypesClassifications",
                columns: new[] { "TypeClassificationId", "Libelle" },
                values: new object[] { 2, "Résidence" });

            migrationBuilder.InsertData(
                table: "TypesClassifications",
                columns: new[] { "TypeClassificationId", "Libelle" },
                values: new object[] { 3, "Immeuble" });

            migrationBuilder.InsertData(
                table: "TypesPromoteurs",
                columns: new[] { "TypePromoteurId", "Libelle" },
                values: new object[] { 1, "Professionnel" });

            migrationBuilder.InsertData(
                table: "TypesPromoteurs",
                columns: new[] { "TypePromoteurId", "Libelle" },
                values: new object[] { 2, "Entreprise" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TypesBiensImmos",
                keyColumn: "TypeBienImmoId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TypesClassifications",
                keyColumn: "TypeClassificationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypesClassifications",
                keyColumn: "TypeClassificationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypesClassifications",
                keyColumn: "TypeClassificationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypesPromoteurs",
                keyColumn: "TypePromoteurId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypesPromoteurs",
                keyColumn: "TypePromoteurId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Lon",
                table: "Adresses");
        }
    }
}
