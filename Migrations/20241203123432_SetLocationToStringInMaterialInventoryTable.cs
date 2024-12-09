using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class SetLocationToStringInMaterialInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "MaterialInventories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHPqrnp+xfXAN/WMkhzrpW+f0AHeyUEu7oaPV5a5MROj78xeNj9eXmk8DUwrlReToQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEFpSihxpmhFSV8LZSnY068cYdncUcPqr1fisb69y8W+EIh0ukRTNVa0q3HPJ6aOjQw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENklInrT2A7W9VKs+O5g8W7oU7je9JdeQ8LO/w7Y2p8jOUQ6O1rf4YS/Dd6yvO6n9g==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEKCC9OXrRmgwU58b0oX9r2GF7Qa1w1e2PXAFMT48idWiwthn4XluL35ozia2mdpGwQ==");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MaterialInventory_MaterialInventoryLocation",
                table: "MaterialInventories",
                sql: "Location IN ('Store', 'PreperationRoom', 'ProductionLine')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MaterialInventory_MaterialInventoryLocation",
                table: "MaterialInventories");

            migrationBuilder.AlterColumn<int>(
                name: "Location",
                table: "MaterialInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }
    }
}
