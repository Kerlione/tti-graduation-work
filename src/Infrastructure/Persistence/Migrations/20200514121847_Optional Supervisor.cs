using Microsoft.EntityFrameworkCore.Migrations;

namespace tti_graduation_work.Infrastructure.Persistence.Migrations
{
    public partial class OptionalSupervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SupervisorId",
                table: "GraduationPapers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SupervisorId",
                table: "GraduationPapers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
