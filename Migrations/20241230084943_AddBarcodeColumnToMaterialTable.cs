using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddBarcodeColumnToMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEHpohhG05RCc0tIq3eABWX4D2Y5I5l2g583icsZSBFCnjylTVA2CLwqTVl8CJy+K+Q==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEAZ59CYSrjhHgaQ8ZXjXd+euMvP6w2xnuwBAhi/DAF14yLmPXHGC1SfXh5HPUwxbLw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEBLmL9ADK+4Dz7F6Gr2rm8OHxaE8394lcFyK3bYRAC2bw3HUQH0csNn6lszVSZCnYg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEH+bIwc41HjXlokKPNGm9cSSzolF9aZYKgBDnrwz5OIQHYYE86j+E0cHkua2nSt/hg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Materials");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEJqTFzahQdx0zoclOvB+sPpRBV4ZvjClIjYriqCrdmR82aIjZ5r7xwbtv5F+6/t5Sg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAED6NoLZWW2akwcKkc4FSRsNPbIzdd8SmTfF6dsrn/+UGcqIcWwMjBB1dRUsZ4lcd8w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGOU1pptuZczwmJpqlj5+9jqUnkJXf9aCo4C7gwhQhwiJY8G8xZZpEejMnXwiVHa1A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAIAAYagAAAAEGND7jCj7xy7p9XoW08CoeJZoLlxtP8sivTN2CqWWbtTUfSUls42GktU8VPvWMRpaA==");
        }
    }
}
