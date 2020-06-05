using Microsoft.EntityFrameworkCore.Migrations;

namespace InquirerOrbind_Back_end.Migrations
{
    public partial class Questions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "MultepleContextTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    second_title = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    count_like = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_QuestionId",
                table: "MultepleContextTable",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultepleContextTable_Questions_QuestionId",
                table: "MultepleContextTable",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultepleContextTable_Questions_QuestionId",
                table: "MultepleContextTable");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_MultepleContextTable_QuestionId",
                table: "MultepleContextTable");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "MultepleContextTable");
        }
    }
}
