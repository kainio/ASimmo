using Microsoft.EntityFrameworkCore.Migrations;

namespace ASimmo.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    AdresseId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quartier = table.Column<string>(nullable: true),
                    CodePostal = table.Column<string>(nullable: true),
                    Ville = table.Column<string>(nullable: true),
                    AdressePostale = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.AdresseId);
                });

            migrationBuilder.CreateTable(
                name: "TypesBiensImmos",
                columns: table => new
                {
                    TypeBienImmoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesBiensImmos", x => x.TypeBienImmoId);
                });

            migrationBuilder.CreateTable(
                name: "TypesClassifications",
                columns: table => new
                {
                    TypeClassificationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesClassifications", x => x.TypeClassificationId);
                    table.ForeignKey(
                        name: "FK_TypesClassifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypesPromoteurs",
                columns: table => new
                {
                    TypePromoteurId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPromoteurs", x => x.TypePromoteurId);
                });

            migrationBuilder.CreateTable(
                name: "Promoteurs",
                columns: table => new
                {
                    PromoteurId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promoteurs", x => x.PromoteurId);
                    table.ForeignKey(
                        name: "FK_Promoteurs_TypesPromoteurs_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypesPromoteurs",
                        principalColumn: "TypePromoteurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Promoteurs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    ClassificationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    PromoteurId = table.Column<int>(nullable: false),
                    Recherchable = table.Column<bool>(nullable: false),
                    PrixMax = table.Column<decimal>(nullable: false),
                    PrixMin = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.ClassificationId);
                    table.ForeignKey(
                        name: "FK_Classifications_Classifications_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Classifications",
                        principalColumn: "ClassificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classifications_Promoteurs_PromoteurId",
                        column: x => x.PromoteurId,
                        principalTable: "Promoteurs",
                        principalColumn: "PromoteurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classifications_TypesClassifications_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypesClassifications",
                        principalColumn: "TypeClassificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locaux",
                columns: table => new
                {
                    LocalId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: false),
                    PromoteurId = table.Column<int>(nullable: false),
                    AdresseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locaux", x => x.LocalId);
                    table.ForeignKey(
                        name: "FK_Locaux_Adresses_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresses",
                        principalColumn: "AdresseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locaux_Promoteurs_PromoteurId",
                        column: x => x.PromoteurId,
                        principalTable: "Promoteurs",
                        principalColumn: "PromoteurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BiensImmos",
                columns: table => new
                {
                    BienImmoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prix = table.Column<decimal>(nullable: false),
                    Surface = table.Column<decimal>(nullable: false),
                    NombreChambre = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    ClassificationId = table.Column<int>(nullable: false),
                    AdresseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiensImmos", x => x.BienImmoId);
                    table.ForeignKey(
                        name: "FK_BiensImmos_Adresses_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresses",
                        principalColumn: "AdresseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BiensImmos_Classifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classifications",
                        principalColumn: "ClassificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BiensImmos_TypesBiensImmos_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypesBiensImmos",
                        principalColumn: "TypeBienImmoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiensImmos_AdresseId",
                table: "BiensImmos",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_BiensImmos_ClassificationId",
                table: "BiensImmos",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_BiensImmos_TypeId",
                table: "BiensImmos",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classifications_ParentId",
                table: "Classifications",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Classifications_PromoteurId",
                table: "Classifications",
                column: "PromoteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Classifications_TypeId",
                table: "Classifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Locaux_AdresseId",
                table: "Locaux",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Locaux_PromoteurId",
                table: "Locaux",
                column: "PromoteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Promoteurs_TypeId",
                table: "Promoteurs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Promoteurs_UserId",
                table: "Promoteurs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesClassifications_UserId",
                table: "TypesClassifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiensImmos");

            migrationBuilder.DropTable(
                name: "Locaux");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropTable(
                name: "TypesBiensImmos");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Promoteurs");

            migrationBuilder.DropTable(
                name: "TypesClassifications");

            migrationBuilder.DropTable(
                name: "TypesPromoteurs");
        }
    }
}
