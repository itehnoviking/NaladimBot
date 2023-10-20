using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaladimBot.Data.Migrations
{
    public partial class AddedNameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Numbers");

            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Names_Numbers_NumberId",
                        column: x => x.NumberId,
                        principalTable: "Numbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Names_NumberId",
                table: "Names",
                column: "NumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Numbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
