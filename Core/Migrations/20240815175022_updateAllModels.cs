using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class updateAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_TestQuestions_QuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestions_TrainingCourse_CourseId",
                table: "TestQuestions");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TrainingVideos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TrainingCourse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramStatus",
                table: "TrainingCourse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TestResults",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "TestQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TestQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "TestQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TestQuestions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramStatus",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProgram",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "AnswerOptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Option",
                table: "AnswerOptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AnswerOptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyMotto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingVideos_CompanyId",
                table: "TrainingVideos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourse_CompanyId",
                table: "TrainingCourse",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_CompanyId",
                table: "TestResults",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_CompanyId",
                table: "TestQuestions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_CompanyId",
                table: "AnswerOptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedById",
                table: "Companies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_Companies_CompanyId",
                table: "AnswerOptions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_TestQuestions_QuestionId",
                table: "AnswerOptions",
                column: "QuestionId",
                principalTable: "TestQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestions_Companies_CompanyId",
                table: "TestQuestions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestions_TrainingCourse_CourseId",
                table: "TestQuestions",
                column: "CourseId",
                principalTable: "TrainingCourse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Companies_CompanyId",
                table: "TestResults",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingCourse_Companies_CompanyId",
                table: "TrainingCourse",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingVideos_Companies_CompanyId",
                table: "TrainingVideos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_Companies_CompanyId",
                table: "AnswerOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_TestQuestions_QuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestions_Companies_CompanyId",
                table: "TestQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestions_TrainingCourse_CourseId",
                table: "TestQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Companies_CompanyId",
                table: "TestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingCourse_Companies_CompanyId",
                table: "TrainingCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingVideos_Companies_CompanyId",
                table: "TrainingVideos");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_TrainingVideos_CompanyId",
                table: "TrainingVideos");

            migrationBuilder.DropIndex(
                name: "IX_TrainingCourse_CompanyId",
                table: "TrainingCourse");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_CompanyId",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestions_CompanyId",
                table: "TestQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AnswerOptions_CompanyId",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TrainingVideos");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TrainingCourse");

            migrationBuilder.DropColumn(
                name: "ProgramStatus",
                table: "TrainingCourse");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TestQuestions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ProgramStatus",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsProgram",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AnswerOptions");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "TestQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TestQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "TestQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "AnswerOptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Option",
                table: "AnswerOptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_TestQuestions_QuestionId",
                table: "AnswerOptions",
                column: "QuestionId",
                principalTable: "TestQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestions_TrainingCourse_CourseId",
                table: "TestQuestions",
                column: "CourseId",
                principalTable: "TrainingCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
