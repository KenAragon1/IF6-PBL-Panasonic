using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndQrCodeUrlColumnToMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QrCodeUrl",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENZvTtpQS9YNrjS8DrERvQwnB1UZZdCoypj+NJmh1kdPhJP2GQcSM6d4Oc2HzLQbBw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDF/hMhxzT1ZT2t1k1oSIgqqV26ULVUzGKpIwP0J1P+nNqDaKNhS84oCT5s52tpkfA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJGWWaTGM4QfYWiui2CUQDA3D6dJs+n9kDn+zdmT/9wkiI+JqFP8ClASo47snCiqXw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJ7U8tqI4k+VIUJ1aFTXYrSjRmF2ZlauCNK7VOYJbEtl1MTcerhZ8TqCcmNTAO3W2A==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "QrCodeUrl",
                table: "Materials");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJC3Q7++iLw3ESXrJlw8Eob/o1HjrN05NlIe+baxYj/hfPLXhJCgrWKGnOrJuDGcUA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGO9R/V4KIaddziNdoy0BhhRYmIuf0B1dXRowZfSqlgIWhILVdq34Nf/1eC6rWGl+w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEDWGuGTQtb1RnCThp52Kc8PP9VRBn+TKb6grMJYjkPWMjmeJ7WCvV2LVeCwsKMAqfg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEO4lNZ8QBSD33wPxgr/kLXy9rDU7nLDLFcNAiv02mrRflKU31Lv30eqCttS5SboZWg==");
        }
    }
}
