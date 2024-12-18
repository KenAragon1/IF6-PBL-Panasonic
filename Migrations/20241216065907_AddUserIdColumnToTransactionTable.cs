using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdColumnToTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MaterialTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEIlkukCWp7sLPZdO5qyMjqZSVjW6oD7CJcY96/Lxo47mEXQEqu2RVdV2y/HL/4nTVA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEN/AOLx/0X6O4TGXpaHxBVX2HDhX9ntmbmChI5jRxeXqUIXg+JnRuMkajzqo07EGWw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEG7UJdqXqCbcrKVuykGJVNTVyj9SuS/6Jrln1c6gEydbidU8uB52Pqxu9ZL2EfdWkw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEEidQTbqXWKzn3wU817I0GdE5unRxlhfB0ndqXxlH9+6iSVTkpPgcMC762ZsFQQcMw==");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTransactions_UserId",
                table: "MaterialTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialTransactions_Users_UserId",
                table: "MaterialTransactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialTransactions_Users_UserId",
                table: "MaterialTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MaterialTransactions_UserId",
                table: "MaterialTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MaterialTransactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEKfDa++TVNj/9GDEUc23mkRAYL3TAT995PMVfelQ0BMVDnnw99axSAAc09HEOFn/+A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEC1qWfaCgYbL/31Vl6WPNTZw1Zm0lYC7XZdGU1J/0vlJjc1IAuLSRGDXrNgv/vW5nw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAECy60xwZb3vIQsonJ3/NW3tcJNWpWR7PLVJNRaWhFM74+pv6z+MD1YjyUuX6rWUbdw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAENuKnA4CH5SoNIsbzYjdDogYMgCR4yjinGBWMJVdPUxQo2Tc6wW2GGB7L0VObrzOeg==");
        }
    }
}
