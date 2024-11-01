using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndRoleTableWithSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DisplayName", "RoleName" },
                values: new object[,]
                {
                    { 1, "Guest", "Guest" },
                    { 2, "Admin", "Admin" },
                    { 3, "Store Manager", "StoreManager" },
                    { 4, "Shift Leader", "ShiftLeader" },
                    { 5, "Asistant Leader", "AsistantLeader" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "EmployeeID", "Fullname", "HashedPassword", "IsVerified", "RoleId" },
                values: new object[,]
                {
                    { 1, "admin@email.com", 301010, "Admin", "AQAAAAIAAYagAAAAEJ3FC2c74C+Xhqx/qlBpZFSFDRdZTjoSzgQxeOLaxp7XFsBO+T8LpvHOOIxkygh/mA==", true, 2 },
                    { 2, "asistantleader@email.com", 301011, "Asistant Leader", "AQAAAAIAAYagAAAAEPXcmBoEfcJkny1YX+9IaQsLJohC5dqOImo84zoLdZBrMZfw7s6X8GbGrRWnsQ/WYA==", true, 5 },
                    { 3, "shiftleader@email.com", 301012, "Shift Leader", "AQAAAAIAAYagAAAAEBCVxgxxdSRwO47D6lx4XalJgDkgbGihLdEgjRNdBdQ9pcMv72sw9/6F6amvoQImDA==", true, 4 },
                    { 4, "storemanager@email.com", 301013, "Store Manager", "AQAAAAIAAYagAAAAELVIGutIdZqPsVdt5/IVOe+vWo8l+FRkX0gQ4vwxW9W2LkpWAgcksFgDfZTLLlq2hg==", true, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
