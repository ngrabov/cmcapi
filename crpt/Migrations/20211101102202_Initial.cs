using Microsoft.EntityFrameworkCore.Migrations;

namespace crpt.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datum",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    symbol = table.Column<string>(type: "TEXT", nullable: true),
                    max_supply = table.Column<double>(type: "REAL", nullable: true),
                    circulating_supply = table.Column<double>(type: "REAL", nullable: false),
                    cmc_rank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datum", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Datum");
        }
    }
}
