using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class CreateMaterialInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestedAt",
                table: "MaterialRequests",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "MaterialInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    ProductionLineId = table.Column<int>(type: "int", nullable: true),
                    StagingProductionLineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialInventories_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialInventories_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialInventories_ProductionLines_StagingProductionLineId",
                        column: x => x.StagingProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAELjRwx4nk/TG4YGfDS0y8L5I6oZhp8yUB825N0h62FgwS+bMnCuA9sEIuRGvhCeFgg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEOuhqN5U1D0Rt18oh7INvwSbYQp2aLJoi//WvPzr2FP4yaBLJrOwME8IZTIrdhrFjw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDrhfBmDQ2PNfh4amXsDMFuU14TuV9dkGgxsFUOrfFYwtxze+cd1ZYQ0kbSVD9T7dA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAECYbl97iIFXTnnoh0N0Lg8sMFvh3cwOd1DznUPYFem7BYQm+s3jTwCDCuqyk3d5SJg==");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInventories_MaterialId",
                table: "MaterialInventories",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInventories_ProductionLineId",
                table: "MaterialInventories",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInventories_StagingProductionLineId",
                table: "MaterialInventories",
                column: "StagingProductionLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialInventories");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "RequestedAt",
                table: "MaterialRequests",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEKNf7WVC1/iSigaKyEBA0Rxg2MxWPuJc9OcR6NtLSXbQ2VrwyeKtGlOuLvubKfV4ww==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJN79dFQ9aNUoCrU7dngwEyphBTJ2fFuDlrHnkOQn6FgkBOHEcP74IQLeBUI+YN3PQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHSAi69r7CwrWpYLOF2nHSa4D79GHmnEyRj0wzrPGuklVHaJoYPsmIJJTplc5omfMA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEPFpZkoB3EX8UZxBY+Q5HsbGv+bfRsVV945h0+juF7m5s1uLoaeInS6Tk4Dt9RDWFQ==");
        }
    }
}
