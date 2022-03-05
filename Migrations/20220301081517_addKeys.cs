using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.Migrations
{
    public partial class addKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberTrainings",
                table: "MemberTrainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberMeetings",
                table: "MemberMeetings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberTrainings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberMeetings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberTrainings",
                table: "MemberTrainings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberMeetings",
                table: "MemberMeetings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTrainings_MemberId",
                table: "MemberTrainings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberMeetings_MemberId",
                table: "MemberMeetings",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberTrainings",
                table: "MemberTrainings");

            migrationBuilder.DropIndex(
                name: "IX_MemberTrainings_MemberId",
                table: "MemberTrainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberMeetings",
                table: "MemberMeetings");

            migrationBuilder.DropIndex(
                name: "IX_MemberMeetings_MemberId",
                table: "MemberMeetings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberTrainings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberMeetings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberTrainings",
                table: "MemberTrainings",
                columns: new[] { "MemberId", "TrainingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberMeetings",
                table: "MemberMeetings",
                columns: new[] { "MemberId", "MeetingId" });
        }
    }
}
