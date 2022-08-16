using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkConsole.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team_Name",
                table: "CricketPlayers");

            migrationBuilder.RenameColumn(
                name: "Team_Id",
                table: "CricketPlayers",
                newName: "TeamId");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CricketPlayers_TeamId",
                table: "CricketPlayers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_CricketPlayers_Teams_TeamId",
                table: "CricketPlayers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CricketPlayers_Teams_TeamId",
                table: "CricketPlayers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_CricketPlayers_TeamId",
                table: "CricketPlayers");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "CricketPlayers",
                newName: "Team_Id");

            migrationBuilder.AddColumn<string>(
                name: "Team_Name",
                table: "CricketPlayers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
