using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalleproducto_marca_MarcaId",
                table: "detalleproducto");

            migrationBuilder.DropTable(
                name: "marca");

            migrationBuilder.DropIndex(
                name: "IX_detalleproducto_MarcaId",
                table: "detalleproducto");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "detalleproducto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "detalleproducto",
                type: "int",
                nullable: true);

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
                name: "IX_detalleproducto_MarcaId",
                table: "detalleproducto",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_detalleproducto_marca_MarcaId",
                table: "detalleproducto",
                column: "MarcaId",
                principalTable: "marca",
                principalColumn: "Id");
        }
    }
}
