using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Update_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WrongAnswer_Question_QuestionId",
                table: "WrongAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_WrongAnswer_User_UserId",
                table: "WrongAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WrongAnswer",
                table: "WrongAnswer");

            migrationBuilder.RenameTable(
                name: "WrongAnswer",
                newName: "FailQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_WrongAnswer_UserId",
                table: "FailQuestion",
                newName: "IX_FailQuestion_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WrongAnswer_QuestionId",
                table: "FailQuestion",
                newName: "IX_FailQuestion_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FailQuestion",
                table: "FailQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FailQuestion_Question_QuestionId",
                table: "FailQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FailQuestion_User_UserId",
                table: "FailQuestion",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailQuestion_Question_QuestionId",
                table: "FailQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_FailQuestion_User_UserId",
                table: "FailQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FailQuestion",
                table: "FailQuestion");

            migrationBuilder.RenameTable(
                name: "FailQuestion",
                newName: "WrongAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_FailQuestion_UserId",
                table: "WrongAnswer",
                newName: "IX_WrongAnswer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FailQuestion_QuestionId",
                table: "WrongAnswer",
                newName: "IX_WrongAnswer_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WrongAnswer",
                table: "WrongAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrongAnswer_Question_QuestionId",
                table: "WrongAnswer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WrongAnswer_User_UserId",
                table: "WrongAnswer",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
