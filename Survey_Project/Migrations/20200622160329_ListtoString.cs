using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey_Project.Migrations
{
    public partial class ListtoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "082b4ad5-a3b1-4186-8a08-043893f769b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3107888-7bf4-4376-9652-582734d66d1d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "700246dd-9d06-4df4-897e-5d4b6e26c0a6", "f43c7f95-0cc4-430d-ac7c-8510e7349161", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82a032e2-62de-4e49-8e36-9d2d9ad67b91", "cfc0aa22-f329-4b3e-afd1-a9d44d977018", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "700246dd-9d06-4df4-897e-5d4b6e26c0a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82a032e2-62de-4e49-8e36-9d2d9ad67b91");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "082b4ad5-a3b1-4186-8a08-043893f769b9", "59096aa8-d754-404d-962d-b41b0ca4127f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3107888-7bf4-4376-9652-582734d66d1d", "5c422c0b-6c4b-4830-b00f-2023c5e0815a", "Customer", "CUSTOMER" });
        }
    }
}
