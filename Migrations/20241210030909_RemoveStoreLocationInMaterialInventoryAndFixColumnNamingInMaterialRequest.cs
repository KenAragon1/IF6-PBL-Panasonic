using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStoreLocationInMaterialInventoryAndFixColumnNamingInMaterialRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MaterialInventory_MaterialInventoryLocation",
                table: "MaterialInventories");

            migrationBuilder.DropColumn(
                name: "AprrovedById",
                table: "MaterialRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedAt",
                table: "MaterialRequests",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "MaterialRequests",
                newName: "RequestedQuantity");

            migrationBuilder.AddColumn<int>(
                name: "FullfilledQuantity",
                table: "MaterialRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredAt",
                table: "MaterialRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGJEbe9mSqPbKuOP4wzAyPkKYd/EdRFjLj7GMieMECi0wLDG8v7nnakYR62z6KrpuA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEEZ1wIDR8XGpX/9YMNw4UwwI3+rctCg1Vv3yNPXe9tHWgk5OtxxTQlA3Ql5WZdbUzA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEH+KI8bGEniRzGbMfhowTmNV1f3rJA64apbnFE4HdmiCyum69ofjZNs7L9XFGf0Dpg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDJt9n5gb+P4Apr4DlHUK0jGlirTfp9N+xMYl14FD5gRCjALzChmvSz/96fmddnMew==");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MaterialInventory_MaterialInventoryLocation",
                table: "MaterialInventories",
                sql: "Location IN ('PreperationRoom', 'ProductionLine')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MaterialInventory_MaterialInventoryLocation",
                table: "MaterialInventories");

            migrationBuilder.DropColumn(
                name: "FullfilledQuantity",
                table: "MaterialRequests");

            migrationBuilder.DropColumn(
                name: "RequiredAt",
                table: "MaterialRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedQuantity",
                table: "MaterialRequests",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "MaterialRequests",
                newName: "RequestedAt");

            migrationBuilder.AddColumn<int>(
                name: "AprrovedById",
                table: "MaterialRequests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEM8q/fZLPWBqDjrcG2lvBWyngyNxWLL4qrw9OG9gjgBdn9nxCnbtPABhvo8J3yLeaw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEC9k5DdFYCXQDoTV2uax7Nklesiny/QFS1KUcoxlNAgjIuD2/7y6hNNrYfyXEL84KA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEL17Xa9qaf/JVpH0QZgoZYMprzisxgyzkkElRP5Il5lWPcLrkyAyer72pDN/ZiuX0Q==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEAj68Da8Jzh0E+ErralR2WnPL6nIw/JGOYOsRwOQkHYJlz5OvACWxGHwT0MdGObI8w==");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MaterialInventory_MaterialInventoryLocation",
                table: "MaterialInventories",
                sql: "Location IN ('Store', 'PreperationRoom', 'ProductionLine')");
        }
    }
}
