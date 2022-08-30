using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "marca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marca", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_productos_GeneroId",
                table: "productos",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_MarcaId",
                table: "productos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_Generos_GeneroId",
                table: "productos",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productos_marca_MarcaId",
                table: "productos",
                column: "MarcaId",
                principalTable: "marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_Generos_GeneroId",
                table: "productos");

            migrationBuilder.DropForeignKey(
                name: "FK_productos_marca_MarcaId",
                table: "productos");

            migrationBuilder.DropTable(
                name: "marca");

            migrationBuilder.DropIndex(
                name: "IX_productos_GeneroId",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_MarcaId",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "productos");
        }
    }
}
