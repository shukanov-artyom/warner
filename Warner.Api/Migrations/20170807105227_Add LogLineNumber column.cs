using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warner.Api.Migrations
{
    public partial class AddLogLineNumbercolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WarningType",
                table: "BuildWarning",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SourceFileName",
                table: "BuildWarning",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogLineNumber",
                table: "BuildWarning",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogLineNumber",
                table: "BuildWarning");

            migrationBuilder.AlterColumn<string>(
                name: "WarningType",
                table: "BuildWarning",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "SourceFileName",
                table: "BuildWarning",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
