using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalCourseworkTemplate.Migrations
{
    public partial class fullQual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinChiPass",
                table: "Qualifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumOfChi",
                table: "Qualifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParOrChi",
                table: "Qualifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parent",
                table: "Qualifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Cadets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    RegisterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attended = table.Column<bool>(nullable: false),
                    DateOfReg = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.RegisterId);
                });

            migrationBuilder.CreateTable(
                name: "CadetRegisters",
                columns: table => new
                {
                    CadetRegisterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CadetId = table.Column<int>(nullable: false),
                    RegisterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadetRegisters", x => x.CadetRegisterId);
                    table.ForeignKey(
                        name: "FK_CadetRegisters_Cadets_CadetId",
                        column: x => x.CadetId,
                        principalTable: "Cadets",
                        principalColumn: "CadetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CadetRegisters_Registers_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Registers",
                        principalColumn: "RegisterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CadetRegisters_CadetId",
                table: "CadetRegisters",
                column: "CadetId");

            migrationBuilder.CreateIndex(
                name: "IX_CadetRegisters_RegisterId",
                table: "CadetRegisters",
                column: "RegisterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadetRegisters");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropColumn(
                name: "MinChiPass",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "NumOfChi",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "ParOrChi",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "Parent",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Cadets");
        }
    }
}
