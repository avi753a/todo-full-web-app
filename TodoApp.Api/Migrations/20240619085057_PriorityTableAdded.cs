using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class PriorityTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Priority_Priority",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Status_Status",
                table: "TaskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Priority",
                table: "Priority");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "TaskStatuses");

            migrationBuilder.RenameTable(
                name: "Priority",
                newName: "TaskPriorities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskStatuses",
                table: "TaskStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskPriorities",
                table: "TaskPriorities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskPriorities_Priority",
                table: "TaskItem",
                column: "Priority",
                principalTable: "TaskPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskStatuses_Status",
                table: "TaskItem",
                column: "Status",
                principalTable: "TaskStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskPriorities_Priority",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskStatuses_Status",
                table: "TaskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskStatuses",
                table: "TaskStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskPriorities",
                table: "TaskPriorities");

            migrationBuilder.RenameTable(
                name: "TaskStatuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "TaskPriorities",
                newName: "Priority");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Priority",
                table: "Priority",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Priority_Priority",
                table: "TaskItem",
                column: "Priority",
                principalTable: "Priority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Status_Status",
                table: "TaskItem",
                column: "Status",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
