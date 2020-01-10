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

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
