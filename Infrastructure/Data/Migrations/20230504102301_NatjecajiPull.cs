using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NatjecajiPull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Natjecaji_Users_MestarID",
                table: "Natjecaji");

            migrationBuilder.DropIndex(
                name: "IX_Natjecaji_MestarID",
                table: "Natjecaji");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Natjecaji");

            migrationBuilder.DropColumn(
                name: "MestarID",
                table: "Natjecaji");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Natjecaji");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NatjecajId = table.Column<Guid>(type: "uuid", nullable: false),
                    MestarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Users_MestarId",
                        column: x => x.MestarId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_MestarId",
                table: "Offers",
                column: "MestarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Finished",
                table: "Natjecaji",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MestarID",
                table: "Natjecaji",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Natjecaji",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Natjecaji_MestarID",
                table: "Natjecaji",
                column: "MestarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Natjecaji_Users_MestarID",
                table: "Natjecaji",
                column: "MestarID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
