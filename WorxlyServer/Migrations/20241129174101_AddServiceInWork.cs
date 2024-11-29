using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorxlyServer.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceInWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "WorkRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecords_ServiceId",
                table: "WorkRecords",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRecords_Services_ServiceId",
                table: "WorkRecords",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkRecords_Services_ServiceId",
                table: "WorkRecords");

            migrationBuilder.DropIndex(
                name: "IX_WorkRecords_ServiceId",
                table: "WorkRecords");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "WorkRecords");
        }
    }
}
