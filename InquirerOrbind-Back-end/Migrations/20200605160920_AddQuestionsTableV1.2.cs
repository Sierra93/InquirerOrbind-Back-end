﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace InquirerOrbind_Back_end.Migrations
{
    public partial class AddQuestionsTableV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UserDetails",
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
                    table.PrimaryKey("PK_UserDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    number = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MultepleContextTable",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<int>(nullable: false),
                    DetailId = table.Column<int>(nullable: false),
                    DetailUserId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultepleContextTable", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_UserDetails_DetailUserId",
                        column: x => x.DetailUserId,
                        principalTable: "UserDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_DetailUserId",
                table: "MultepleContextTable",
                column: "DetailUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_QuestionId",
                table: "MultepleContextTable",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_UserId1",
                table: "MultepleContextTable",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultepleContextTable");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
