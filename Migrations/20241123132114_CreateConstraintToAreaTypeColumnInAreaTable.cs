using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class CreateConstraintToAreaTypeColumnInAreaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDP37x9W0Yw+Bu8Xw7q+a7j3w9k6ZMyJAeTb7JQuDKtXThKyaXkgKdm0g7pt0TrPlQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHsnFuCNGmvaC/r1mey/370HubkeW/eyY21pMKO5HffkAlKnMUVs059+y3d6tHp8vA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGNVhEtUELdqL6tlppVvDn1yT+H8x4BLB9zBEWOd7vBWproqphmr36EDlEOBEj4Tew==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDE1Q5a25cBlovauzbPwMKQ5OEwNiMyPAV4InDLkDR3W2N0oJlmxstPVchF7kdOtZA==");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Area_AreaType",
                table: "Areas",
                sql: "Type IN ('PreperationRoom', 'ProductionLine', 'Store')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Area_AreaType",
                table: "Areas");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDmebvIsLhR6ZZt4vybuN6bikdNtBMV2QgdnO9v1eDQiUHqZp4ngMtQc7tsp5BaFQA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAELryv8eo6xkIYMoIV6LoNgErYV3uvct+lxCf109/lpAjXWkyHWcpfHxTfwhdM3Fj8w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEN+RBXGlLK9KgL5rmtBm3F8tQDVmDVvAT0IvmLHwo2SJiNKsPS2eLlzyW/c8Z1MYEQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEK8K90/BzOMWpOJtyJ4AkfvvKykUM4lMob2pkpgK9UPhTIbeVcMFagrHNqHRPXybIQ==");
        }
    }
}
