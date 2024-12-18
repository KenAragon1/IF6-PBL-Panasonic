using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panasonic.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterialTransactionDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialTransactions_Materials_MaterialId",
                table: "MaterialTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MaterialTransactions_MaterialId",
                table: "MaterialTransactions");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "MaterialTransactions");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MaterialTransactions");

            migrationBuilder.CreateTable(
                name: "MaterialTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTransactionDetails_MaterialTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "MaterialTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialTransactionDetails_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTransactionDetails_MaterialId",
                table: "MaterialTransactionDetails",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTransactionDetails_TransactionId",
                table: "MaterialTransactionDetails",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialTransactionDetails");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "MaterialTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MaterialTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTransactions_MaterialId",
                table: "MaterialTransactions",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialTransactions_Materials_MaterialId",
                table: "MaterialTransactions",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
