using Microsoft.EntityFrameworkCore.Migrations;

namespace tti_graduation_work.Infrastructure.Persistence.Migrations
{
    public partial class SupervisorLanguagesJobPositionstablerename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Supervisors_SupervisorId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_JobPosition_JobPositionId",
                table: "Supervisors");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_Languages_LanguageId",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisors_LanguageId",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Languages_SupervisorId",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPosition",
                table: "JobPosition");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "JobPosition",
                newName: "JobPositions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPositions",
                table: "JobPositions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SupervisorLanguages",
                columns: table => new
                {
                    SupervisorId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorLanguages", x => new { x.SupervisorId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SupervisorLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorLanguages_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorLanguages_LanguageId",
                table: "SupervisorLanguages",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_JobPositions_JobPositionId",
                table: "Supervisors",
                column: "JobPositionId",
                principalTable: "JobPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_JobPositions_JobPositionId",
                table: "Supervisors");

            migrationBuilder.DropTable(
                name: "SupervisorLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPositions",
                table: "JobPositions");

            migrationBuilder.RenameTable(
                name: "JobPositions",
                newName: "JobPosition");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Supervisors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Languages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPosition",
                table: "JobPosition",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_LanguageId",
                table: "Supervisors",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_SupervisorId",
                table: "Languages",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Supervisors_SupervisorId",
                table: "Languages",
                column: "SupervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_JobPosition_JobPositionId",
                table: "Supervisors",
                column: "JobPositionId",
                principalTable: "JobPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_Languages_LanguageId",
                table: "Supervisors",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
