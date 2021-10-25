using Microsoft.EntityFrameworkCore.Migrations;

namespace CyclingResults.Migrations
{
    public partial class AddingUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Races",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ResultUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    RaceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultUploads_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultUploads_RaceId",
                table: "ResultUploads",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races");

            migrationBuilder.DropTable(
                name: "ResultUploads");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Races",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Events_EventId",
                table: "Races",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
