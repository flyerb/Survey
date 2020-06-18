using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey_Project.Migrations
{
    public partial class Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e7c5cd6-7594-4ca9-b09e-d4f6c1465098");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a962dfb3-4f3a-4274-b966-cad4f59cc2c6");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Surveys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "886bb26b-b8b7-4761-b658-50cb913dbf79", "93305af9-a888-42dd-ac8d-0fd335f73d71", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60f0296b-cceb-4644-aae7-f594131f92a2", "54c90f93-79bb-498e-8822-f9e2a4cdf6cb", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_AdminId",
                table: "Surveys",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_IdentityUserId",
                table: "Admins",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Admins_AdminId",
                table: "Surveys",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Admins_AdminId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_AdminId",
                table: "Surveys");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60f0296b-cceb-4644-aae7-f594131f92a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "886bb26b-b8b7-4761-b658-50cb913dbf79");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Surveys");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a962dfb3-4f3a-4274-b966-cad4f59cc2c6", "fdba7c2e-3374-4d41-8c7a-21b6fc28d982", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e7c5cd6-7594-4ca9-b09e-d4f6c1465098", "6d8179c5-94f6-47d6-a675-9a233130c896", "Customer", "CUSTOMER" });
        }
    }
}
