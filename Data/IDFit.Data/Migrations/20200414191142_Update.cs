using Microsoft.EntityFrameworkCore.Migrations;

namespace IDFit.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercosesTools");

            migrationBuilder.CreateTable(
                name: "ExercisesTools",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false),
                    ToolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesTools", x => new { x.ExerciseId, x.ToolId });
                    table.ForeignKey(
                        name: "FK_ExercisesTools_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExercisesTools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesTools_ToolId",
                table: "ExercisesTools",
                column: "ToolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisesTools");

            migrationBuilder.CreateTable(
                name: "ExercosesTools",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercosesTools", x => new { x.ExerciseId, x.ToolId });
                    table.ForeignKey(
                        name: "FK_ExercosesTools_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExercosesTools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercosesTools_ToolId",
                table: "ExercosesTools",
                column: "ToolId");
        }
    }
}
