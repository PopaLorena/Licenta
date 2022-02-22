using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Meetings_MeetingId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Trainings_TrainingId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MeetingId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_TrainingId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FacilitatorId",
                table: "Meetings");

            migrationBuilder.AddColumn<string>(
                name: "TrainerName",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberModelId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilitatorName",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MeetingMemberModel",
                columns: table => new
                {
                    MeetingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingMemberModel", x => new { x.MeetingsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_MeetingMemberModel_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingMemberModel_Members_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberModelTraining",
                columns: table => new
                {
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberModelTraining", x => new { x.ParticipantsId, x.TrainingsId });
                    table.ForeignKey(
                        name: "FK_MemberModelTraining_Members_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberModelTraining_Trainings_TrainingsId",
                        column: x => x.TrainingsId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MemberModelId",
                table: "Tasks",
                column: "MemberModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMemberModel_ParticipantsId",
                table: "MeetingMemberModel",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberModelTraining_TrainingsId",
                table: "MemberModelTraining",
                column: "TrainingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_MemberModelId",
                table: "Tasks",
                column: "MemberModelId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_MemberModelId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "MeetingMemberModel");

            migrationBuilder.DropTable(
                name: "MemberModelTraining");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_MemberModelId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TrainerName",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "MemberModelId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FacilitatorName",
                table: "Meetings");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MeetingId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FacilitatorId",
                table: "Meetings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Members_MeetingId",
                table: "Members",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_TrainingId",
                table: "Members",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Meetings_MeetingId",
                table: "Members",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Trainings_TrainingId",
                table: "Members",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
