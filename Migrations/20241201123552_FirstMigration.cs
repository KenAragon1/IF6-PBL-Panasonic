using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailQuantity = table.Column<int>(type: "int", nullable: false),
                    DetailMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.Id);
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
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "ShiftLeader"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CK_User_UserRole", "Role IN ('ShiftLeader', 'AsistantLeader', 'StoreManager', 'Admin', 'MaterialHandler', 'Guest')");
                });

            migrationBuilder.CreateTable(
                name: "MaterialRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Pending"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    VerifiedById = table.Column<int>(type: "int", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AprrovedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RejectedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequests", x => x.Id);
                    table.CheckConstraint("CK_MaterialRequest_MaterialRequestStatus", "Status IN ('Pending', 'Verified', 'Approved', 'Rejected', 'Completed')");
                    table.ForeignKey(
                        name: "FK_MaterialRequests_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialRequests_Users_RejectedById",
                        column: x => x.RejectedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialRequests_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_Users_VerifiedById",
                        column: x => x.VerifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "EmployeeID", "Fullname", "HashedPassword", "IsVerified", "Role" },
                values: new object[,]
                {
                    { 1, "admin@email.com", 301010, "Admin", "AQAAAAIAAYagAAAAEMQ08kcbDEEDxKH6pqsAW7f3l8RISHam4K5jY4SKzS5CcziK3DMeKYpZNOputm5qSQ==", true, "Admin" },
                    { 2, "asistantleader@email.com", 301011, "Asistant Leader", "AQAAAAIAAYagAAAAEPqjgvN/WJZk7oXjan7JMGEqUWbODvjof0Z5M1DUE3gaCMJXpNLTZabIDCp8d3i8Uw==", true, "AsistantLeader" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "EmployeeID", "Fullname", "HashedPassword", "IsVerified" },
                values: new object[] { 3, "shiftleader@email.com", 301012, "Shift Leader", "AQAAAAIAAYagAAAAEOARSloRmwjd4u7iAEB7bpwuNkDUvu8o8te6LwgDp/Hk0zMw58toKr/Z2D961Zehtg==", true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "EmployeeID", "Fullname", "HashedPassword", "IsVerified", "Role" },
                values: new object[] { 4, "storemanager@email.com", 301013, "Store Manager", "AQAAAAIAAYagAAAAEP1DlQMubeBSeQjqcxTnXd3fITsJer9RNcCkTFCwWPYdVaTPVm24OkN+ytU9/mDqSQ==", true, "StoreManager" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_ApprovedById",
                table: "MaterialRequests",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_MaterialId",
                table: "MaterialRequests",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_ProductionLineId",
                table: "MaterialRequests",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_RejectedById",
                table: "MaterialRequests",
                column: "RejectedById");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_RequestedById",
                table: "MaterialRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_VerifiedById",
                table: "MaterialRequests",
                column: "VerifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_Number",
                table: "Materials",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLines_Remark",
                table: "ProductionLines",
                column: "Remark",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialRequests");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "ProductionLines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
