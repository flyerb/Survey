using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey_Project.Migrations
{
    public partial class QuestionsViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60f0296b-cceb-4644-aae7-f594131f92a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "886bb26b-b8b7-4761-b658-50cb913dbf79");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Option",
                table: "Options");

            migrationBuilder.AddColumn<string>(
                name: "QuestionString",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Choice",
                table: "Options",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "082b4ad5-a3b1-4186-8a08-043893f769b9", "59096aa8-d754-404d-962d-b41b0ca4127f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3107888-7bf4-4376-9652-582734d66d1d", "5c422c0b-6c4b-4830-b00f-2023c5e0815a", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "082b4ad5-a3b1-4186-8a08-043893f769b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3107888-7bf4-4376-9652-582734d66d1d");

            migrationBuilder.DropColumn(
                name: "QuestionString",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Choice",
                table: "Options");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Option",
                table: "Options",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "886bb26b-b8b7-4761-b658-50cb913dbf79", "93305af9-a888-42dd-ac8d-0fd335f73d71", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60f0296b-cceb-4644-aae7-f594131f92a2", "54c90f93-79bb-498e-8822-f9e2a4cdf6cb", "Customer", "CUSTOMER" });
        }
    }
}
