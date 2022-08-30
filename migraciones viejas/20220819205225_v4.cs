using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_detalleproducto",
                table: "detalleproducto");

            migrationBuilder.DropIndex(
                name: "IX_detalleproducto_colorId",
                table: "detalleproducto");

            migrationBuilder.DropIndex(
                name: "IX_detalleproducto_tallaId",
                table: "detalleproducto");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "detalleproducto",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalleproducto",
                table: "detalleproducto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_detalleproducto_colorId",
                table: "detalleproducto",
                column: "colorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalleproducto_productoId",
                table: "detalleproducto",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleproducto_tallaId",
                table: "detalleproducto",
                column: "tallaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_detalleproducto",
                table: "detalleproducto");

            migrationBuilder.DropIndex(
                name: "IX_detalleproducto_colorId",
                table: "detalleproducto");

            migrationBuilder.DropIndex(
                name: "IX_detalleproducto_productoId",
                table: "detalleproducto");

            migrationBuilder.DropIndex(
                name: "IX_detalleproducto_tallaId",
                table: "detalleproducto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "detalleproducto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalleproducto",
                table: "detalleproducto",
                columns: new[] { "productoId", "tallaId", "colorId" });

            migrationBuilder.CreateIndex(
                name: "IX_detalleproducto_colorId",
                table: "detalleproducto",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleproducto_tallaId",
                table: "detalleproducto",
                column: "tallaId");
        }
    }
}
