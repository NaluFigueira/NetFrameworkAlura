using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _2_UsuarioAPI.Migrations
{
    public partial class CreatingCustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "561c5cbd-5a4d-4277-96b8-fa28009761b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "dce409f5-8702-4c40-a1da-ae14a2e0eb44");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca10202a-627d-4a93-b81f-6a8ca4c6ec6a", "AQAAAAEAACcQAAAAEDSALV4REqrHRPgRplJVz/AjVK9v7dk69f0RT31bLggep8PDN768ARXCFyU/UGMnYw==", "15ac1f71-48c7-495b-9623-91e05c111dda" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "efd74408-67d2-4be8-8be9-ac4026033c8a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "a4fdacec-7ed3-4857-a3fd-ccab789ba310");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de37ef90-6249-4d3b-b6a9-b5c0197eda26", "AQAAAAEAACcQAAAAEPWm9euFItzXvFGYM8b9axqYDMzUDHrgM9YbbP73NKEARdXi/DGBZUhUeGn5rhcWjg==", "46de0073-1ac0-46bc-87c9-aa47ba83012c" });
        }
    }
}
