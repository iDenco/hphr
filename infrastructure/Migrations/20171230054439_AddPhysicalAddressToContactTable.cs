using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace infrastructure.Migrations
{
    public partial class AddPhysicalAddressToContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "physical_city",
                table: "contacts",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "physical_country",
                table: "contacts",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "physical_state",
                table: "contacts",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "physical_street",
                table: "contacts",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "physical_zip_code",
                table: "contacts",
                maxLength: 9,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "physical_city",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "physical_country",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "physical_state",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "physical_street",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "physical_zip_code",
                table: "contacts");
        }
    }
}
