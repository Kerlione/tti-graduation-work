using Microsoft.EntityFrameworkCore.Migrations;

namespace tti_graduation_work.Infrastructure.Persistence.Migrations
{
    public partial class SupervisorTopicsandFieldOfInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldsOfInterest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_RU = table.Column<string>(maxLength: 1024, nullable: true),
                    Title_EN = table.Column<string>(maxLength: 1024, nullable: true),
                    Title_LV = table.Column<string>(maxLength: 1024, nullable: true),
                    SupervisorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldsOfInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldsOfInterest_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThesisTopics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_RU = table.Column<string>(maxLength: 1024, nullable: true),
                    Title_EN = table.Column<string>(maxLength: 1024, nullable: true),
                    Title_LV = table.Column<string>(maxLength: 1024, nullable: true),
                    SupervisorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThesisTopics_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldsOfInterest_SupervisorId",
                table: "FieldsOfInterest",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisTopics_SupervisorId",
                table: "ThesisTopics",
                column: "SupervisorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldsOfInterest");

            migrationBuilder.DropTable(
                name: "ThesisTopics");
        }
    }
}
