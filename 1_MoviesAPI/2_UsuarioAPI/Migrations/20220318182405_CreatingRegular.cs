using Microsoft.EntityFrameworkCore.Migrations;

namespace _2_UsuarioAPI.Migrations
{
    public partial class CreatingRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "a4fdacec-7ed3-4857-a3fd-ccab789ba310");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "efd74408-67d2-4be8-8be9-ac4026033c8a", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de37ef90-6249-4d3b-b6a9-b5c0197eda26", "AQAAAAEAACcQAAAAEPWm9euFItzXvFGYM8b9axqYDMzUDHrgM9YbbP73NKEARdXi/DGBZUhUeGn5rhcWjg==", "46de0073-1ac0-46bc-87c9-aa47ba83012c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "fce05a7b-8f44-44a8-8564-2a62e02eefc0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cba664d-84ff-488a-8c11-9b89198c45e4", "AQAAAAEAACcQAAAAEFtxUVUd8AOdqF3KbCAlpcI9Tx/uXdP6f7EKdojzoWzcBJHL7k0gFFvWLn/SYoT8QA==", "628f275f-9900-4b38-b0f9-723e8030b946" });
        }
    }
}
