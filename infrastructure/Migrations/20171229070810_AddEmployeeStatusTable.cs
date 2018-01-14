using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace infrastructure.Migrations
{
    public partial class AddEmployeeStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employee_status",
                table: "employees",
                newName: "status_id");

            migrationBuilder.AddColumn<int>(
                name: "employee_status_id",
                table: "employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "employee_statuses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(maxLength: 7, nullable: false),
                    description = table.Column<string>(maxLength: 15, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_statuses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_employee_status_id",
                table: "employees",
                column: "employee_status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_employee_statuses_employee_status_id",
                table: "employees",
                column: "employee_status_id",
                principalTable: "employee_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_employee_statuses_employee_status_id",
                table: "employees");

            migrationBuilder.DropTable(
                name: "employee_statuses");

            migrationBuilder.DropIndex(
                name: "ix_employees_employee_status_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "employee_status_id",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "employees",
                newName: "employee_status");
        }
    }
}
