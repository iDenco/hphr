using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace infrastructure.Migrations
{
    public partial class UpdateDefaultValueForIsUnderProbation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_under_probation",
                table: "employees",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_under_probation",
                table: "employees",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
