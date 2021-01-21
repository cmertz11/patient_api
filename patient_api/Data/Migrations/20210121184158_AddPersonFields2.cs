using Microsoft.EntityFrameworkCore.Migrations;

namespace patient_api.Data.Migrations
{
    public partial class AddPersonFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "PatientAddresss");

            migrationBuilder.RenameIndex(
                name: "IX_Address_PatientId",
                table: "PatientAddresss",
                newName: "IX_PatientAddresss_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientAddresss",
                table: "PatientAddresss",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientAddresss",
                table: "PatientAddresss");

            migrationBuilder.RenameTable(
                name: "PatientAddresss",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAddresss_PatientId",
                table: "Address",
                newName: "IX_Address_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");
        }
    }
}
