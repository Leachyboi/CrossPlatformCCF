using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalCourseworkTemplate.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualName = table.Column<string>(nullable: true),
                    PassMark = table.Column<int>(nullable: false),
                    ParOrChi = table.Column<string>(nullable: true),
                    Parent = table.Column<string>(nullable: true),
                    NumChi = table.Column<int>(nullable: false),
                    MinChiPass = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Qualification");
        }
    }
}
