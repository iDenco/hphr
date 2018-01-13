using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace infrastructure.Migrations
{
    public partial class AddEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "contacts",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobile_phone",
                table: "contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "official_email",
                table: "contacts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    abaaccount_name = table.Column<string>(maxLength: 150, nullable: true),
                    abaaccount_number = table.Column<string>(maxLength: 20, nullable: true),
                    contact_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    designation_id = table.Column<int>(nullable: false),
                    employee_status = table.Column<int>(nullable: false),
                    employee_type_id = table.Column<int>(nullable: false),
                    ended_probation_at = table.Column<DateTime>(nullable: false),
                    hired_at = table.Column<DateTime>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    is_under_probation = table.Column<bool>(nullable: true),
                    leave_at = table.Column<DateTime>(nullable: false),
                    remarks = table.Column<string>(nullable: true),
                    updated_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_contacts_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employees_designations_designation_id",
                        column: x => x.designation_id,
                        principalTable: "designations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employees_employee_types_employee_type_id",
                        column: x => x.employee_type_id,
                        principalTable: "employee_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_contact_id",
                table: "employees",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_designation_id",
                table: "employees",
                column: "designation_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_employee_type_id",
                table: "employees",
                column: "employee_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "employee_types");

            migrationBuilder.DropColumn(
                name: "mobile_phone",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "official_email",
                table: "contacts");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "contacts",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
