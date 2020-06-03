using Microsoft.EntityFrameworkCore.Migrations;

namespace InquirerOrbind_Back_end.Migrations
{
    public partial class InitConfigurationStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "number",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "MultepleContextTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DetailUserId",
                table: "MultepleContextTable",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDetailы",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailы", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_DetailUserId",
                table: "MultepleContextTable",
                column: "DetailUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultepleContextTable_UserDetailы_DetailUserId",
                table: "MultepleContextTable",
                column: "DetailUserId",
                principalTable: "UserDetailы",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultepleContextTable_UserDetailы_DetailUserId",
                table: "MultepleContextTable");

            migrationBuilder.DropTable(
                name: "UserDetailы");

            migrationBuilder.DropIndex(
                name: "IX_MultepleContextTable_DetailUserId",
                table: "MultepleContextTable");

            migrationBuilder.DropColumn(
                name: "number",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "MultepleContextTable");

            migrationBuilder.DropColumn(
                name: "DetailUserId",
                table: "MultepleContextTable");
        }
    }
}
