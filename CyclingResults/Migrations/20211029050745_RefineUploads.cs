using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CyclingResults.Migrations
{
    public partial class RefineUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultUploads_Races_RaceId",
                table: "ResultUploads");

            migrationBuilder.AlterColumn<int>(
                name: "RaceId",
                table: "ResultUploads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "ResultUploads",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "ResultId",
                table: "ResultUploads",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultUploads_Races_RaceId",
                table: "ResultUploads",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultUploads_Races_RaceId",
                table: "ResultUploads");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ResultUploads");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "ResultUploads");

            migrationBuilder.AlterColumn<int>(
                name: "RaceId",
                table: "ResultUploads",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultUploads_Races_RaceId",
                table: "ResultUploads",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
