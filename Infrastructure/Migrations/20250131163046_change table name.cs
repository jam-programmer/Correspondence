using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changetablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentEntity_Letter_LetterId",
                table: "AttachmentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttachmentEntity",
                table: "AttachmentEntity");

            migrationBuilder.RenameTable(
                name: "AttachmentEntity",
                newName: "Attachment");

            migrationBuilder.RenameIndex(
                name: "IX_AttachmentEntity_LetterId",
                table: "Attachment",
                newName: "IX_Attachment_LetterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachment",
                table: "Attachment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Letter_LetterId",
                table: "Attachment",
                column: "LetterId",
                principalTable: "Letter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Letter_LetterId",
                table: "Attachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachment",
                table: "Attachment");

            migrationBuilder.RenameTable(
                name: "Attachment",
                newName: "AttachmentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Attachment_LetterId",
                table: "AttachmentEntity",
                newName: "IX_AttachmentEntity_LetterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttachmentEntity",
                table: "AttachmentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentEntity_Letter_LetterId",
                table: "AttachmentEntity",
                column: "LetterId",
                principalTable: "Letter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
