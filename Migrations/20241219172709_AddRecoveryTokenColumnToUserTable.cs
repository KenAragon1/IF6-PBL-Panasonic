using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddRecoveryTokenColumnToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialInventories_ProductionLines_StagingProductionLineId",
                table: "MaterialInventories");

            migrationBuilder.DropIndex(
                name: "IX_MaterialInventories_StagingProductionLineId",
                table: "MaterialInventories");

            migrationBuilder.DropColumn(
                name: "StagingProductionLineId",
                table: "MaterialInventories");

            migrationBuilder.AddColumn<string>(
                name: "RecoveryToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HashedPassword", "RecoveryToken", "TokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEJqTFzahQdx0zoclOvB+sPpRBV4ZvjClIjYriqCrdmR82aIjZ5r7xwbtv5F+6/t5Sg==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HashedPassword", "RecoveryToken", "TokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAED6NoLZWW2akwcKkc4FSRsNPbIzdd8SmTfF6dsrn/+UGcqIcWwMjBB1dRUsZ4lcd8w==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HashedPassword", "RecoveryToken", "TokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEGOU1pptuZczwmJpqlj5+9jqUnkJXf9aCo4C7gwhQhwiJY8G8xZZpEejMnXwiVHa1A==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HashedPassword", "RecoveryToken", "TokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEGND7jCj7xy7p9XoW08CoeJZoLlxtP8sivTN2CqWWbtTUfSUls42GktU8VPvWMRpaA==", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenExpiry",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "StagingProductionLineId",
                table: "MaterialInventories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJht/xBO7emsecjK9YpeQlzyJ1iSNZHEd3neYVKyfasOq+aYSEH6tq7FZyqSF3JgIQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPNq87+P3d0ZAOu94ViMaDaCqWqK7eEuXDSUSJiTQ/5ZudwVxR4ZK4TcPhwqj01g/A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEMmgWFx2pQmP6r1sTo0a5msSNnNyKuceAwEkeME2YlPdMCMdaGgFf9yfL06vdYdx5A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEMAx+EdA32TXxgcjKoigrA8MjOWIdfJ+HuzOXc5IleJcqBrfYRkLyizeS7KVr03uKg==");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInventories_StagingProductionLineId",
                table: "MaterialInventories",
                column: "StagingProductionLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialInventories_ProductionLines_StagingProductionLineId",
                table: "MaterialInventories",
                column: "StagingProductionLineId",
                principalTable: "ProductionLines",
                principalColumn: "Id");
        }
    }
}
