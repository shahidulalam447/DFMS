using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firm.Infrastructure.Migrations
{
    public partial class dfms_a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "869d8842-5d08-479f-b569-7765f3e4985c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7211",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e48b4d1b-c76c-4866-8326-7c250e1ff7e5", "Guest", "GUEST" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f04028e-587c-47ad-8b36-6dbd6a059fa4",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PhoneNumber", "PreFix", "UserName" },
                values: new object[] { "dfms@gmail.com", "dfms@gmail.com", "DFMS", "01775204284", "dfms123456", "DFMS" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f04028e-587c-48ad-8b36-6dbd6a059fa5",
                columns: new[] { "Email", "Name", "NormalizedEmail", "NormalizedUserName", "PhoneNumber", "PreFix", "UserName", "UserType" },
                values: new object[] { "guest@gmail.com", "System Guest", "guest@gmail.com", "GUEST", "01601709945", "Guest123456", "Guest", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4098dbe0-996f-43f0-8020-7c26a84ec7dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7211",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "db39bcc5-4648-459d-a724-19efa1b36a6d", "Hotel", "HOTEL" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f04028e-587c-47ad-8b36-6dbd6a059fa4",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PhoneNumber", "PreFix", "UserName" },
                values: new object[] { "tclagro@gmail.com", "tclagro@gmail.com", "DFMS", "01700729807", "tcl123456", "tcl" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f04028e-587c-48ad-8b36-6dbd6a059fa5",
                columns: new[] { "Email", "Name", "NormalizedEmail", "NormalizedUserName", "PhoneNumber", "PreFix", "UserName", "UserType" },
                values: new object[] { "dil.afroza@krishibidgroup.com", null, "dil.afroza@krishibidgroup.com", "HOTEL", "01700729808", "Ji123456", "Hotel", 3 });
        }
    }
}
