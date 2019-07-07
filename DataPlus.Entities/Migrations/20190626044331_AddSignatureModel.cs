using Microsoft.EntityFrameworkCore.Migrations;

namespace DataPlus.Entities.Migrations
{
    public partial class AddSignatureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentSignature_Signature_SignatureId",
                table: "EnrollmentSignature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Signature",
                table: "Signature");

            migrationBuilder.RenameTable(
                name: "Signature",
                newName: "Signatures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Signatures",
                table: "Signatures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentSignature_Signatures_SignatureId",
                table: "EnrollmentSignature",
                column: "SignatureId",
                principalTable: "Signatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentSignature_Signatures_SignatureId",
                table: "EnrollmentSignature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Signatures",
                table: "Signatures");

            migrationBuilder.RenameTable(
                name: "Signatures",
                newName: "Signature");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Signature",
                table: "Signature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentSignature_Signature_SignatureId",
                table: "EnrollmentSignature",
                column: "SignatureId",
                principalTable: "Signature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
