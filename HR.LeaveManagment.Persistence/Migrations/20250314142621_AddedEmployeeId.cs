using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_leaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.RenameColumn(
                name: "leaveTypeId",
                table: "LeaveAllocations",
                newName: "LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_leaveTypeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_LeaveTypeId");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.RenameColumn(
                name: "LeaveTypeId",
                table: "LeaveAllocations",
                newName: "leaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_LeaveTypeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_leaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_leaveTypeId",
                table: "LeaveAllocations",
                column: "leaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
