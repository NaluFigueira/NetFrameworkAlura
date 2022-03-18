using Microsoft.EntityFrameworkCore.Migrations;

namespace _2_UsuarioAPI.Migrations
{
    public partial class CreatingAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99999, "fce05a7b-8f44-44a8-8564-2a62e02eefc0", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 99999, 0, "9cba664d-84ff-488a-8c11-9b89198c45e4", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEFtxUVUd8AOdqF3KbCAlpcI9Tx/uXdP6f7EKdojzoWzcBJHL7k0gFFvWLn/SYoT8QA==", null, false, "628f275f-9900-4b38-b0f9-723e8030b946", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 99999, 99999 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 99999, 99999 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999);
        }
    }
}
