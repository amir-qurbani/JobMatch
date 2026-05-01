using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobMatch.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsToMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matches_JobId",
                table: "Matches",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_JobSeekerId",
                table: "Matches",
                column: "JobSeekerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_JobSeekers_JobSeekerId",
                table: "Matches",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Jobs_JobId",
                table: "Matches",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_JobSeekers_JobSeekerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Jobs_JobId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_JobId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_JobSeekerId",
                table: "Matches");
        }
    }
}
