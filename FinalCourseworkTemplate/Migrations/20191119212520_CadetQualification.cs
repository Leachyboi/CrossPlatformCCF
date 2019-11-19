using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalCourseworkTemplate.Migrations
{
    public partial class CadetQualification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Qualification",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "MinChiPass",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "NumChi",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "ParOrChi",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "Parent",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "QualName",
                table: "Qualification");

            migrationBuilder.RenameTable(
                name: "Qualification",
                newName: "Qualifications");

            migrationBuilder.AddColumn<int>(
                name: "QualificationId",
                table: "Qualifications",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Qualifications",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qualifications",
                table: "Qualifications",
                column: "QualificationId");

            migrationBuilder.CreateTable(
                name: "Cadets",
                columns: table => new
                {
                    CadetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(nullable: true),
                    KnownAs = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Form = table.Column<string>(nullable: true),
                    Platoon = table.Column<int>(nullable: false),
                    Section = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadets", x => x.CadetId);
                });

            migrationBuilder.CreateTable(
                name: "CadetQualifications",
                columns: table => new
                {
                    CadetQualificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CadetId = table.Column<int>(nullable: false),
                    QualificationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadetQualifications", x => x.CadetQualificationId);
                    table.ForeignKey(
                        name: "FK_CadetQualifications_Cadets_CadetId",
                        column: x => x.CadetId,
                        principalTable: "Cadets",
                        principalColumn: "CadetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CadetQualifications_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CadetQualifications_CadetId",
                table: "CadetQualifications",
                column: "CadetId");

            migrationBuilder.CreateIndex(
                name: "IX_CadetQualifications_QualificationId",
                table: "CadetQualifications",
                column: "QualificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadetQualifications");

            migrationBuilder.DropTable(
                name: "Cadets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Qualifications",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "QualificationId",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Qualifications");

            migrationBuilder.RenameTable(
                name: "Qualifications",
                newName: "Qualification");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Qualification",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MinChiPass",
                table: "Qualification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumChi",
                table: "Qualification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParOrChi",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parent",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualName",
                table: "Qualification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qualification",
                table: "Qualification",
                column: "ID");
        }
    }
}
