using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace infrastructure.Migrations
{
    public partial class UpdateEmployeeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_employee_statuses_employee_status_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "employees");

            migrationBuilder.AlterColumn<int>(
                name: "employee_status_id",
                table: "employees",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_employee_statuses_employee_status_id",
                table: "employees",
                column: "employee_status_id",
                principalTable: "employee_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_employee_statuses_employee_status_id",
                table: "employees");

            migrationBuilder.AlterColumn<int>(
                name: "employee_status_id",
                table: "employees",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_employee_statuses_employee_status_id",
                table: "employees",
                column: "employee_status_id",
                principalTable: "employee_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
